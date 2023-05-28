namespace FilmRating.Features.Authentication;

public interface IUserProvider
{
    string? GetUserId();
}