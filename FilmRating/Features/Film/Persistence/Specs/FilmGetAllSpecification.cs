using FilmRating.Infrastructure.Repository;

namespace FilmRating.Features.Film;

public class FilmGetAllSpecification : BaseSpecification<FilmEntity>
{
    public FilmGetAllSpecification(bool withGenre = false, bool withDirector = false, bool withActors = false)
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

        ApplyOrderByDescending(x => x.Rating);
    }
}