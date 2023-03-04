using FilmRating.Infrastructure.Injection;
using Mapster;
using MapsterMapper;
using static System.Reflection.Assembly;

namespace FilmRating.Infrastructure.Mapping;

public class MappingInjectModule: IInjectModule
{
    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        
        var registers = config.Scan(GetExecutingAssembly());
        
        config.Apply(registers);
        
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
    }
}