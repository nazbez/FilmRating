using System.ComponentModel.DataAnnotations;

namespace FilmRating.Features.Authentication;

public class LoginModel
{
    [Required]
    [EmailAddress] 
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}