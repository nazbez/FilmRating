using JetBrains.Annotations;
using Mapster;

namespace FilmRating.Features.Film.Artist;

public record ArtistVm(
    Guid Id,
    string FirstName,
    string LastName,
    IEnumerable<ArtistRoleVm> Roles)
{
    [UsedImplicitly]
    public class MapperConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ArtistEntity, ArtistVm>(); 
        }
    }
}

public record ArtistRoleVm(int Id, string Name)
{
    [UsedImplicitly]
    public class MapperConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ArtistRoleEntity, ArtistRoleVm>();
        }
    }
}
    
    