using FilmRating.Infrastructure.Injection;
using FilmRating.Persistence.Sql;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FilmRating.Infrastructure.Repository;

[UsedImplicitly]
public class RepositoryInjectModule : IInjectModule
{
    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FilmRatingDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Sql")));
        
        services.AddDefaultIdentity<IdentityUser>()
            .AddEntityFrameworkStores<FilmRatingDbContext>();
        
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
    }
}