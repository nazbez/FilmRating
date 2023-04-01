using JetBrains.Annotations;
using Mapster;

namespace FilmRating.Features.Film;

public record FilmVm(
    int Id,
    string Title,
    int Year,
    double Rating,
    string Genre)
{
    [UsedImplicitly]
    public class MapperConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<FilmEntity, FilmVm>()
                .Map(dst => dst.Genre, src => src.Genre!.Name);
        }
    }
}