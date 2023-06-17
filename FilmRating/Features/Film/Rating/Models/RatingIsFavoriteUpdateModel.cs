using System.ComponentModel.DataAnnotations;

namespace FilmRating.Features.Film.Rating;

public record RatingisFavoriteUpdateModel(
    [Required] int FilmId,
    [Required] bool IsFavorite);