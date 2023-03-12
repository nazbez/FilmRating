using FilmRating.Infrastructure.Repository;

namespace FilmRating.Features.Film.Artist;

public class ArtistGetAllSpecification : BaseSpecification<ArtistEntity>
{
    public ArtistGetAllSpecification()
    {
        AddInclude(a => a.Roles);
    }
}