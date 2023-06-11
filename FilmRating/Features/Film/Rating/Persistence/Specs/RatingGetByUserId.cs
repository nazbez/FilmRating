using FilmRating.Infrastructure.Repository;

namespace FilmRating.Features.Film.Rating;

public class RatingGetByUserId : BaseSpecification<RatingEntity>
{
    public RatingGetByUserId(string userId)
        : base(x => x.UserId == userId)
    {
        AddInclude(e => e.Film!);
    }
}