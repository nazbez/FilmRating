using JetBrains.Annotations;
using Mapster;

namespace FilmRating.Features.Film;

public record FilmSearchResultModel(
    int Id,
    string Title,
    int Year,
    double Rating,
    string? PhotoPath,
    string Genre)
{
    [UsedImplicitly]
    public class MapperConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<FilmEntity, FilmSearchResultModel>();
        }
    }
}