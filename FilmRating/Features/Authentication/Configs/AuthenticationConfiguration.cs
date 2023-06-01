using System.Text;

namespace FilmRating.Features.Authentication;

public class AuthenticationConfiguration
{
    public string Key { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public int ExpiryInHours { get; set; }
    public GoogleAuthenticationConfiguration GoogleAuthenticationConfiguration { get; set; } = null!;

    public byte[] EncodedKey => Encoding.UTF8.GetBytes(Key);
}

public class GoogleAuthenticationConfiguration
{
    public string ClientId { get; set; } = null!;
}