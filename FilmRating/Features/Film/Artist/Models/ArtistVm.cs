using Mapster;

namespace FilmRating.Features.Film.Artist;

public record ArtistVm(
    Guid Id,
    string FirstName,
    string LastName,
    IEnumerable<ArtistRoleVm> Roles)
{
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
    public class MapperConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ArtistRoleEntity, ArtistRoleVm>();
        }
    }
}
    
    