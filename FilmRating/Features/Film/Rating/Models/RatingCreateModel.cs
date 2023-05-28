using System.ComponentModel.DataAnnotations;

namespace FilmRating.Features.Film.Rating;

public record RatingCreateModel(
    [Required] int FilmId,
    [Required] int Rate);