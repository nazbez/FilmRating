using System.ComponentModel.DataAnnotations;

namespace FilmRating.Features.Film.Rating;

public record RatingIsFavoriteCreateModel(
    [Required] int FilmId,
    [Required] bool IsFavorite);