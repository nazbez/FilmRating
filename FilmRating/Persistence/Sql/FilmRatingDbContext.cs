using FilmRating.Infrastructure.Repository;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Assembly;

namespace FilmRating.Persistence.Sql;

public class FilmRatingDbContext : IdentityDbContext
{
    public FilmRatingDbContext(DbContextOptions<FilmRatingDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = GetExecutingAssembly();
        base.OnModelCreating(modelBuilder);
        modelBuilder.RegisterAllEntities<IEntity>(assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
}