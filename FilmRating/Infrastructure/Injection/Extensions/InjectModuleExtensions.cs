namespace FilmRating.Infrastructure.Injection;

public static class InjectModuleExtensions
{
    public static void RegisterDependencies(this IServiceCollection services, IConfiguration configuration) => 
        typeof(Program).Assembly.ExportedTypes
            .Where(x => typeof(IInjectModule).IsAssignableFrom(x) && x is { IsInterface: false, IsAbstract: false })
            .Select(Activator.CreateInstance)
            .Cast<IInjectModule>()
            .ToList()
            .ForEach(x => x.Register(services, configuration));
}