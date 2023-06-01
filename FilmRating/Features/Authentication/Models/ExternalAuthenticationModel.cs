namespace FilmRating.Features.Authentication;

public class ExternalAuthenticationModel
{
    public string Provider { get; set; } = null!;
    public string IdToken { get; set; } = null!;
}