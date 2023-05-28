using FilmRating.Infrastructure.Repository;

namespace FilmRating.Features.Film.Artist;

public class ArtistGetByRoleSpecification : BaseSpecification<ArtistEntity>
{
    public ArtistGetByRoleSpecification(int roleId) 
        : base(x => x.Roles.Any(r => r.Id == roleId)) { }
}