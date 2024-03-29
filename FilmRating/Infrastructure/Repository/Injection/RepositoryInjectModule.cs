﻿using FilmRating.Features.Authentication;
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
        
        services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<FilmRatingDbContext>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IRepository<,>), typeof(EfRepository<,>));
    }
}