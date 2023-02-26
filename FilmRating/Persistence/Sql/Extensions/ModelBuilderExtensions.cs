using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace FilmRating.Persistence.Sql;

public static class ModelBuilderExtensions
{
    public static void RegisterAllEntities<TEntity>(this ModelBuilder modelBuilder, params Assembly[] assemblies)
    {
        var types = assemblies.SelectMany(a => a.GetExportedTypes())
            .Where(c => c is { IsClass: true, IsAbstract: false, IsPublic: true, IsInterface: false }
                        && typeof(TEntity).IsAssignableFrom(c));

        foreach (var type in types)
            modelBuilder.Entity(type);
    }
}