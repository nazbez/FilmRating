using System.ComponentModel.DataAnnotations;

namespace FilmRating.Features.Film.Rating;

public record RatingUpdateModel(
    [Required] int FilmId,
    [Required] int Rate);