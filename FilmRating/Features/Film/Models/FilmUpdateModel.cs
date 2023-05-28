using System.ComponentModel.DataAnnotations;

namespace FilmRating.Features.Film;

public record FilmUpdateModel(
    [Required] string Title,
    int Year,
    [Required] string ShortDescription,
    decimal Budget,
    int DurationInMinutes,
    IFormFile? Photo,
    int GenreId,
    Guid DirectorId,
    IEnumerable<Guid> ActorIds);