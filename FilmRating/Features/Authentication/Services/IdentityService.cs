using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace FilmRating.Features.Authentication;

public class IdentityService : IIdentityService
{
    private readonly UserManager<IdentityUser> userManager;
    private readonly AuthenticationConfiguration authenticationConfiguration;

    public IdentityService(
        UserManager<IdentityUser> userManager,
        AuthenticationConfiguration authenticationConfiguration)
    {
        this.userManager = userManager;
        this.authenticationConfiguration = authenticationConfiguration;
    }
    
    public async Task<AuthenticationResultModel> Register(RegisterModel model)
    {
        var existingUser = await userManager.FindByEmailAsync(model.Email);

        if (existingUser != null)
        {
            return new AuthenticationResultModel
            {
                ErrorMessages = new[] { "User with this email already exists" }
            };
        }

        var newUser = new IdentityUser
        {
            Email = model.Email,
            UserName = $"{model.FirstName} {model.LastName}",
        };

        var createdUser = await userManager.CreateAsync(newUser, model.Password);

        if (!createdUser.Succeeded)
        {
            return new AuthenticationResultModel
            {
                ErrorMessages = createdUser.Errors.Select(e => e.Description)
            };
        }

        var authenticationResult = GenerateAuthenticationResult(newUser);

        return authenticationResult;
    }

    public async Task<AuthenticationResultModel> Login(string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user == null)
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

        var authenticationResult = GenerateAuthenticationResult(user);
        
        return authenticationResult;
    }

    private AuthenticationResultModel GenerateAuthenticationResult(IdentityUser user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim("id", user.Id),
                new Claim(JwtRegisteredClaimNames.Aud, authenticationConfiguration.Audience),
                new Claim(JwtRegisteredClaimNames.Iss, authenticationConfiguration.Issuer)
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(authenticationConfiguration.EncodedKey), 
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new AuthenticationResultModel
        {
            Success = true,
            Token = tokenHandler.WriteToken(token)
        };
    }
}