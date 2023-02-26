using FilmRating.Infrastructure.Injection;
using JetBrains.Annotations;
using Microsoft.OpenApi.Models;

namespace FilmRating.Infrastructure.Swagger;

[UsedImplicitly]
public class SwaggerInjectModule : IInjectModule
{
    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(option =>
        {
            var security = new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,

                    },
                    new List<string>()
                }
            };
            
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the bearer scheme",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });
            
            option.AddSecurityRequirement(security);
        });
    }
}