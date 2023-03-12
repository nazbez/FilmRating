using FilmRating.Infrastructure.Repository;

namespace FilmRating.Features.Film.Artist;

public class ArtistGetByIdsSpecification : BaseSpecification<ArtistEntity>
{
    public ArtistGetByIdsSpecification(
        IEnumerable<Guid> ids,
        bool withRoles = false,
        bool withActorFilms = false,
        bool withDirectorFilms = false)
        : base(x => ids.Any(id => id == x.Id))
    {
        if (withRoles)
        {
            AddInclude(x => x.Roles);
        }

        if (withActorFilms)
        {
            AddInclude(x => x.ActorFilms);
        }

        if (withDirectorFilms)
        {
            AddInclude(x => x.DirectorFilms);
        }
    }
}