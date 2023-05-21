using FilmRating.Features.Film.Artist;
using FilmRating.Features.Film.Genre;
using JetBrains.Annotations;
using Mapster;

namespace FilmRating.Features.Film;

public record FilmDetailsVm(
    int Id,
    string Title,
    int Year,
    string ShortDescription,
    decimal Budget,
    double Duration,
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
            config.NewConfig<FilmEntity, FilmDetailsVm>()
                .Map(dst => dst.Duration, src => src.Duration.TotalMinutes);
        }
    }
}