using FilmRating.Infrastructure.Injection;
using JetBrains.Annotations;

namespace FilmRating.Features.Film.Rating;

[UsedImplicitly]
public class RatingInjectModule : IInjectModule
{
    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        var ratingConfiguration = new RatingConfiguration();
        configuration.GetSection(nameof(RatingConfiguration))
            .Bind(ratingConfiguration);

        services.AddSingleton(ratingConfiguration);
    }
}