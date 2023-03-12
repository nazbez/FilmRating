using FilmRating.Infrastructure.Repository;

namespace FilmRating.Features.Film;

public class FilmGetByIdSpecification : BaseSpecification<FilmEntity>
{
    public FilmGetByIdSpecification(
        int id, 
        bool withGenre = false, 
        bool withDirector = false,
        bool withActors = false) 
        : base(x => x.Id == id)
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