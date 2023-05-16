using System.ComponentModel.DataAnnotations;

namespace FilmRating.Features.Film.Rating.Models;

public record RatingCreateModel(
    [Required] int FilmId,
    [Required] string UserId,
    [Required] int Rate);