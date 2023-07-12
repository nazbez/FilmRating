namespace FilmRating.Features.Authentication;

public class AuthorizationConfiguration
{
    public IReadOnlyCollection<string> AdminEmails { get; set; } = null!;
}