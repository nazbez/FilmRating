using FilmRating.Features.Authentication;
using FilmRating.Infrastructure.Repository;

namespace FilmRating.Features.Film.Rating;

public class RatingEntity : Entity<int>
{
    public int? Rate { get; private set; }
    public bool IsFavourite { get; private set; }
    public int FilmId { get; private set; }
    public string UserId { get; private set; } = null!;
    public FilmEntity? Film { get; private set; }
    public User? User { get; private set; }

    public void UpdateRate(int rate) =>
        Rate = rate;

    public void UpdateIsFavorite(bool isFavorite) =>
        IsFavourite = isFavorite;

    public static RatingEntity Create(
        int filmId, 
        string userId, 
        int? rate,
        bool isFavorite = false) => 
        new() 
        { 
            FilmId = filmId, 
            UserId = userId, 
            Rate = rate,
            IsFavourite = isFavorite
        };
}