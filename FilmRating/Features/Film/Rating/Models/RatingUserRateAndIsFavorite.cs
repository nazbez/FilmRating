namespace FilmRating.Features.Film.Rating;

public record RatingUserRateAndIsFavorite(bool HasRate, int? Rate, int FilmId, bool IsFavorite = false);