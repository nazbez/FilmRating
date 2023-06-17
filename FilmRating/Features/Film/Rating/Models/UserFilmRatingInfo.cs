namespace FilmRating.Features.Film.Rating;

public record UserFilmRatingInfo(bool HasRate, int? Rate, int FilmId, bool IsFavorite = false);