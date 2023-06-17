using System.ComponentModel.DataAnnotations;

namespace FilmRating.Features.Film.Rating;

public record RatingIsFavouriteUpdateModel(
    [Required] int FilmId,
    [Required] bool IsFavourite);