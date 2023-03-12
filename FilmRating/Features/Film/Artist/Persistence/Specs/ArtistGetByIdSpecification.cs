using FilmRating.Infrastructure.Repository;

namespace FilmRating.Features.Film.Artist;

public class ArtistGetByIdSpecification  : BaseSpecification<ArtistEntity>
{
    public ArtistGetByIdSpecification(
        Guid id,
        bool withRoles = false, 
        bool withActorFilms = false,
        bool withDirectorFilms = false) 
        : base(x => x.Id == id)
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