using FilmRating.Features.Authentication;
using FilmRating.Infrastructure.Repository;

namespace FilmRating.Features.Film.Rating.Persistence;

public class RatingEntity : IEntity<int>
{
    public int Id { get; private set; }
    public int Rate { get; private set; }
    public int FilmId { get; private set; }
    public Guid UserId { get; private set; }
    public FilmEntity? Film { get; private set; } = null!;
    public User? User { get; private set; } = null!;

    public static RatingEntity Create(
    int filmId,
    Guid userId,
    int rate) =>
    new()
    {
        FilmId = filmId,
        UserId = userId,
        Rate = rate
    };
}