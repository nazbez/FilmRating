using System.Reflection;
using FilmRating.Infrastructure.Injection;
using FluentValidation;
using JetBrains.Annotations;
using MediatR;

namespace FilmRating.Infrastructure.Pipeline;

[UsedImplicitly]
public class PipelineInjectModule : IInjectModule
{
    public void Register(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}