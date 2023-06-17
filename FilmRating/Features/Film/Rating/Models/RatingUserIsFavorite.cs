namespace FilmRating.Features.Film.Rating;

public record RatingUserIsFavorite(bool HasRate, bool IsFavorite, int FilmId);