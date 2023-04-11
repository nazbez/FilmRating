// using JetBrains.Annotations;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Design;
//
// namespace FilmRating.Persistence.Sql;
// TODO REMOVE OR NOT?
// [UsedImplicitly]
// public class FilmRatingDbContextFactory : IDesignTimeDbContextFactory<FilmRatingDbContext>
// {
//     public FilmRatingDbContext CreateDbContext(string[] args)
//     {
//         var optionsBuilder = new DbContextOptionsBuilder<FilmRatingDbContext>();    
//         var builder = new ConfigurationBuilder();
//
// 
//         builder.SetBasePath(Directory.GetCurrentDirectory())
//             .AddJsonFile("Configs/appsettings.json", optional: true)
//             .AddJsonFile($"Configs/appsettings.{}.json", reloadOnChange: true, optional: false);      
//         
//         var config = builder.Build();       
//         var connectionString = config.GetConnectionString("Sql");
//         
//         optionsBuilder.UseSqlServer(connectionString);       
//         
//         return new FilmRatingDbContext(optionsBuilder.Options);
//     }
// }