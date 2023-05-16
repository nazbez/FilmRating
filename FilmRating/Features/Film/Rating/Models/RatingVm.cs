using FilmRating.Features.Film.Rating.Persistence;
using JetBrains.Annotations;
using Mapster;

namespace FilmRating.Features.Film.Rating.Models;

public record RatingVm(
    int Id,
    int FilmId,
    string UserId,
    int Rate)
{
    [UsedImplicitly]
    public class MapperConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RatingEntity, RatingVm>();
        }
    }
}