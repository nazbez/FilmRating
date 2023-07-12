using FilmRating.Infrastructure.Injection;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace FilmRating.Features.Authentication;

[UsedImplicitly]
public class AuthenticationInjectModule : IInjectModule
{
    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        var authenticationConfiguration = new AuthenticationConfiguration();
        configuration.GetSection(nameof(AuthenticationConfiguration))
            .Bind(authenticationConfiguration);

        services.AddSingleton(authenticationConfiguration);

        var authorizationConfiguration = new AuthorizationConfiguration();
        configuration.GetSection(nameof(AuthorizationConfiguration))
            .Bind(authorizationConfiguration);

        services.AddSingleton(authorizationConfiguration);

        services.AddScoped<IIdentityService, IdentityService>();

        services.AddScoped<IUserProvider, UserProvider>();

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = authenticationConfiguration.Issuer,
                ValidAudience = authenticationConfiguration.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(authenticationConfiguration.EncodedKey.ToArray())
            };
        });
        
        services.AddScoped<IIdentityService, IdentityService>();
    }
}