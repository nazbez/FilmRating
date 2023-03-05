namespace FilmRating.Features.Film.Artist;

public record ArtistUpdateModel( 
    string FirstName, 
    string LastName, 
    IEnumerable<int> RoleIds);