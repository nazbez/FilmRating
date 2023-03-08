using System.ComponentModel.DataAnnotations;

namespace FilmRating.Features.Film.Artist;

public record ArtistUpdateModel(
    [Required] string FirstName, 
    [Required] string LastName, 
    IEnumerable<int> RoleIds);