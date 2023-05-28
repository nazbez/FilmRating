using FilmRating.Infrastructure.Repository;

namespace FilmRating.Features.Film.Rating;

public class RatingGetByUserIdAndFilmId : BaseSpecification<RatingEntity>
{
    public RatingGetByUserIdAndFilmId(int filmId, string userId) :
        base(x => x.FilmId == filmId && x.UserId == userId) { }
}