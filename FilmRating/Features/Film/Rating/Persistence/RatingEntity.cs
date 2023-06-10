using FilmRating.Features.Authentication;
using FilmRating.Infrastructure.Repository;

namespace FilmRating.Features.Film.Rating;

public class RatingEntity : IEntity<int>
{
    public int Id { get; private set; }
    public int? Rate { get; private set; }
    public bool IsFavourite { get; private set; } = false;
    public int FilmId { get; private set; }
    public string UserId { get; private set; } = null!;
    public FilmEntity? Film { get; private set; }
    public User? User { get; private set; }

    public void UpdateRate(int rate) =>
        Rate = rate;

    public static RatingEntity Create(
        int filmId, 
        string userId, 
        int rate) => 
        new() 
        { 
            FilmId = filmId, 
            UserId = userId, 
            Rate = rate 
        };
    
    public static RatingEntity Create(
        int filmId, 
        string userId) => 
        new() 
        { 
            FilmId = filmId, 
            UserId = userId, 
        };
}