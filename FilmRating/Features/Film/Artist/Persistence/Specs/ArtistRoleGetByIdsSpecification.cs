using FilmRating.Infrastructure.Repository;

namespace FilmRating.Features.Film.Artist;

public class ArtistRoleGetByIdsSpecification : BaseSpecification<ArtistRoleEntity>
{
    public ArtistRoleGetByIdsSpecification(IEnumerable<int> ids) 
        : base(a => ids.Any(i => i == a.Id)) { }
}