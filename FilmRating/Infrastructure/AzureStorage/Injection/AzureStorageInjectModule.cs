using FilmRating.Infrastructure.Injection;

namespace FilmRating.Infrastructure.AzureStorage;

public class AzureStorageInjectModule : IInjectModule
{
    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        var authenticationConfiguration = new AzureStorageConfiguration();
        configuration.GetSection(nameof(AzureStorageConfiguration))
            .Bind(authenticationConfiguration);

        services.AddSingleton(authenticationConfiguration);

        services.AddScoped<IAzureStorageService, AzureStorageService>();
    }
}