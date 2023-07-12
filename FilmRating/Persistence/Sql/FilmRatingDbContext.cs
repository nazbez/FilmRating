using FilmRating.Features.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Assembly;

namespace FilmRating.Persistence.Sql;

public class FilmRatingDbContext : IdentityDbContext<User>
{
    public FilmRatingDbContext(DbContextOptions<FilmRatingDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        var assembly = GetExecutingAssembly();
        base.OnModelCreating(builder);
        builder.RegisterAllEntities(assembly);
        builder.ApplyConfigurationsFromAssembly(assembly);
    }
}