using FilmRating.Infrastructure.Repository;

namespace FilmRating.Features.Film;

public class FilmsGetByIdsSpecification : BaseSpecification<FilmEntity>
{
    public FilmsGetByIdsSpecification(
        IEnumerable<int> ids, 
        bool withGenre = false, 
        bool withDirector = false,
        bool withActors = false) 
        : base(x => ids.Contains(x.Id))
    {
        if (withGenre)
        {
            AddInclude(f => f.Genre!); 
        }

        if (withDirector)
        {
            AddInclude(f => f.Director!);
        }

        if (withActors)
        {
            AddInclude(f => f.Actors);
        }
    }
}