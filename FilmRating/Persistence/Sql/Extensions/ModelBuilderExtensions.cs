using System.Reflection;
using FilmRating.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace FilmRating.Persistence.Sql;

public static class ModelBuilderExtensions
{
    public static void RegisterAllEntities(this ModelBuilder modelBuilder, params Assembly[] assemblies)
    {
        var types = assemblies.SelectMany(a => a.GetExportedTypes())
            .Where(c => c is { IsClass: true, IsAbstract: false, IsPublic: true, IsInterface: false }
                        && c.IsDefined(typeof(ConfigurableEntityAttribute)));

        foreach (var type in types)
            modelBuilder.Entity(type);
    }
}