namespace FilmRating.Features.Authentication;

public interface IIdentityService
{
    Task<AuthenticationResultModel> Register(RegisterModel model);
    Task<AuthenticationResultModel> Login(string email, string password);
    Task<AuthenticationResultModel> ExternalLogin(ExternalAuthenticationModel model);
}