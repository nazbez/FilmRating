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
    string Genre,
    string Director,
    IEnumerable<string> Actors)
{
    [UsedImplicitly]
    public class MapperConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<FilmEntity, FilmDetailsVm>()
                .Map(dst => dst.Genre, src => src.Genre!.Name)
                .Map(dst => dst.Director, src => $"{src.Director!.FirstName} {src.Director!.LastName}")
                .Map(dst => dst.Duration, src => src.Duration.TotalMinutes)
                .Map(dst => dst.Actors, src => src.Actors.Select(a => $"{a.FirstName} {a.LastName}"));
        }
    }
}