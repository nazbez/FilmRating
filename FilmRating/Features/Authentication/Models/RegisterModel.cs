using System.ComponentModel.DataAnnotations;

namespace FilmRating.Features.Authentication;

public class RegisterModel
{
    [Required]
    [EmailAddress] 
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;
}