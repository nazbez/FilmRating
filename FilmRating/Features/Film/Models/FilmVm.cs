using FilmRating.Features.Film.Artist;
using FilmRating.Features.Film.Genre;
using JetBrains.Annotations;
using Mapster;

namespace FilmRating.Features.Film;

public record FilmVm(
    int Id,
    string Title,
    int Year,
    double Rating,
    string? PhotoPath,
    GenreVm Genre,
    ArtistVm Director,
    IEnumerable<ArtistVm> Actors)
{
    [UsedImplicitly]
    public class MapperConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<FilmEntity, FilmVm>();
        }
    }
}