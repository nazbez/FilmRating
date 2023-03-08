using System.ComponentModel.DataAnnotations;

namespace FilmRating.Features.Film.Artist;

public record ArtistCreateModel(
    [Required] string FirstName, 
    [Required] string LastName, 
    IEnumerable<int> RoleIds);