namespace FilmRating.Infrastructure.Injection;

public interface IInjectModule
{
    void Register(IServiceCollection services, IConfiguration configuration);
}