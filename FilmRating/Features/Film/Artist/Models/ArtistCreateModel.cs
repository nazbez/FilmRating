namespace FilmRating.Features.Film.Artist;

public record ArtistCreateModel(
    string FirstName, 
    string LastName, 
    IEnumerable<int> RoleIds);