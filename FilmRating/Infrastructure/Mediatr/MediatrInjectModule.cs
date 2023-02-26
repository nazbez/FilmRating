using FilmRating.Infrastructure.Injection;
using JetBrains.Annotations;

namespace FilmRating.Infrastructure.Mediatr;

[UsedImplicitly]
public class MediatrInjectModule : IInjectModule
{
    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
    }
}