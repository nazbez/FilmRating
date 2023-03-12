using JetBrains.Annotations;
using Mapster;

namespace FilmRating.Features.Film.Genre;

public record GenreVm(int Id, string Name)
{
    [UsedImplicitly]
    public class MapperConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GenreEntity, GenreVm>();
        }
    }
}