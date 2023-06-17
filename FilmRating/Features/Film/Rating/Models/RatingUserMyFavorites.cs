using JetBrains.Annotations;
using Mapster;

namespace FilmRating.Features.Film.Rating;

public record RatingUserMyFavorites(bool IsFavourite, int FilmId, FilmVm Film)
{
    [UsedImplicitly]
    public class MapperConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RatingEntity, RatingUserMyFavorites>();
        }
    }
}