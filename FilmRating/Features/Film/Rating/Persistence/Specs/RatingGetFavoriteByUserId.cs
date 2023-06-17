using FilmRating.Infrastructure.Repository;

namespace FilmRating.Features.Film.Rating;

public class RatingGetFavoriteByUserId : BaseSpecification<RatingEntity>
{
    public RatingGetFavoriteByUserId(string userId)
        : base(x => x.UserId == userId && x.IsFavourite)
    {
    }
}