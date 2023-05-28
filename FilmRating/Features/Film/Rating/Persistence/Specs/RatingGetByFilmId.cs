using FilmRating.Infrastructure.Repository;

namespace FilmRating.Features.Film.Rating;

public class RatingGetByFilmId : BaseSpecification<RatingEntity>
{
    public RatingGetByFilmId(int filmId) 
        : base(x => x.FilmId == filmId) { }
}