namespace FilmRating.Features.Authentication;

public class UserProvider : IUserProvider
{
    private readonly IHttpContextAccessor context;

    public UserProvider(IHttpContextAccessor context)
    {
        this.context = context;
    }

    public string? GetUserId() =>
        context.HttpContext?.User.Claims
            .First(i => i.Type == JwtCustomClaimNames.Id).Value;
}