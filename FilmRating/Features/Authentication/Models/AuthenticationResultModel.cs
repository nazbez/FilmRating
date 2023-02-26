namespace FilmRating.Features.Authentication;

public class AuthenticationResultModel
{
    public string Token { get; set; } = null!;
    public bool Success { get; set; }
    public IEnumerable<string> ErrorMessages { get; set; } = null!; 
}