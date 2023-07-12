using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using static FilmRating.Features.Authentication.UserRoleEntityConstants;

namespace FilmRating.Features.Authentication;

public class IdentityService : IIdentityService
{
    private readonly UserManager<User> userManager;
    private readonly AuthenticationConfiguration authenticationConfiguration;
    private readonly AuthorizationConfiguration authorizationConfiguration;
    private readonly ILogger<IdentityService> logger;

    public IdentityService(
        UserManager<User> userManager,
        AuthenticationConfiguration authenticationConfiguration,
        AuthorizationConfiguration authorizationConfiguration,
        ILogger<IdentityService> logger)
    {
        this.userManager = userManager;
        this.authenticationConfiguration = authenticationConfiguration;
        this.authorizationConfiguration = authorizationConfiguration;
        this.logger = logger;
    }
    
    public async Task<AuthenticationResultModel> Register(RegisterModel model)
    {
        var existingUser = await userManager.FindByEmailAsync(model.Email);

        if (existingUser is not null)
        {
            return new AuthenticationResultModel
            {
                ErrorMessages = new[] { "User with this email already exists" }
            };
        }

        var newUser = new User
        {
            Email = model.Email,
            UserName = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName
        };

        var createdUser = await userManager.CreateAsync(newUser, model.Password);

        if (!createdUser.Succeeded)
        {
            return new AuthenticationResultModel
            {
                ErrorMessages = createdUser.Errors.Select(e => e.Description)
            };
        }

        await AddToRole(newUser);
        
        var authenticationResult = await GenerateAuthenticationResult(newUser);

        return authenticationResult;
    }

    public async Task<AuthenticationResultModel> Login(string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user is null)
        {
            return new AuthenticationResultModel
            {
                ErrorMessages = new[] { "User does not exist" }
            };
        }

        var hasValidPassword = await userManager.CheckPasswordAsync(user, password);

        if (!hasValidPassword)
        {
            return new AuthenticationResultModel
            {
                ErrorMessages = new[] { "Login or password are invalid" }
            };
        }

        var authenticationResult = await GenerateAuthenticationResult(user);
        
        return authenticationResult;
    }

    public async Task<AuthenticationResultModel> ExternalLogin(ExternalAuthenticationModel model)
    {
        try
        {
            var payload = await VerifyGoogleToken(model.IdToken);
            
            var info = new UserLoginInfo(model.Provider, payload.Subject, model.Provider);
            
            var user = await userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            
            if (user is null)
            {
                user = await userManager.FindByEmailAsync(payload.Email);
                
                if (user is null)
                {
                    user = new User
                    {
                        Email = payload.Email, 
                        UserName = payload.Email,
                        FirstName = payload.GivenName,
                        LastName = payload.FamilyName
                    };
                    
                    await userManager.CreateAsync(user);
                    await AddToRole(user);
                    await userManager.AddLoginAsync(user, info);
                }
                else
                {
                    await userManager.AddLoginAsync(user, info);
                }
            }
            
            var authenticationResult = await GenerateAuthenticationResult(user);

            return authenticationResult;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during external login");
            
            return new AuthenticationResultModel
            {
                ErrorMessages = new[] { "Invalid external authentication" }
            };
        }
    }

    private async Task<AuthenticationResultModel> GenerateAuthenticationResult(User user)
    {
        var roles = (await userManager.GetRolesAsync(user))
            .Select(x => new Claim(ClaimTypes.Role, x));
        
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtCustomClaimNames.Id, user.Id),
                new Claim(JwtRegisteredClaimNames.Aud, authenticationConfiguration.Audience),
                new Claim(JwtRegisteredClaimNames.Iss, authenticationConfiguration.Issuer),
            }),
            Expires = DateTime.UtcNow.AddHours(authenticationConfiguration.ExpiryInHours),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(authenticationConfiguration.EncodedKey.ToArray()), 
                SecurityAlgorithms.HmacSha256Signature)
        };
        
        tokenDescriptor.Subject.AddClaims(roles);

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new AuthenticationResultModel
        {
            Success = true,
            Token = tokenHandler.WriteToken(token)
        };
    }
    
    private async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(string idToken)
    {
        try
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new List<string> { authenticationConfiguration.GoogleAuthenticationConfiguration.ClientId }
            };
            
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
            return payload;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error during verifying google token");
            throw;
        }
    }

    private async Task AddToRole(User user)
    {
        if (authorizationConfiguration.AdminEmails.Contains(user.Email))
        {
            await userManager.AddToRoleAsync(user, Administrator);
        }
        else
        {
            await userManager.AddToRoleAsync(user, Critic);
        }
    }
}