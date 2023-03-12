using FilmRating.Infrastructure.Injection;
using JetBrains.Annotations;

namespace FilmRating.Features.Film;

[UsedImplicitly]
public class FilmInjectModule : IInjectModule
{
    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        var filmConfiguration = new FilmConfiguration();
        configuration.GetSection(nameof(FilmConfiguration))
            .Bind(filmConfiguration);
        
        services.AddSingleton(filmConfiguration);
    }
}