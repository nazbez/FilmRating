using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FilmRating.Persistence.Sql;

public class FilmRatingDbContextFactory : IDesignTimeDbContextFactory<FilmRatingDbContext>
{
    public FilmRatingDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FilmRatingDbContext>();    
        var builder = new ConfigurationBuilder();     
        
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("Configs/appsettings.json");      
        
        var config = builder.Build();       
        var connectionString = config.GetConnectionString("Sql");
        
        optionsBuilder.UseSqlServer(connectionString);       
        
        return new FilmRatingDbContext(optionsBuilder.Options);
    }
}