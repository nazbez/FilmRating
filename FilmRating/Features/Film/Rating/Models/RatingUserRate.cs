namespace FilmRating.Features.Film.Rating;

public record RatingUserRate(bool HasRate, int? Rate, int FilmId);