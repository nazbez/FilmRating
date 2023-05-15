using System.ComponentModel.DataAnnotations;

namespace FilmRating.Features.Film.Rating.Models;

public record RatingCreateModel(
    [Required] int FilmId,
    [Required] Guid UserId,
    [Required] int Rate);