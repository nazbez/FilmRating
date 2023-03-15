using Microsoft.AspNetCore.Identity;

namespace FilmRating.Features.Authentication;

public class User : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}