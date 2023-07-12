using System.Reflection;
using MediatR;

namespace FilmRating.Infrastructure.Pipeline;

public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> logger;
    
    public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
    {
        this.logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling {RequestName}", typeof(TRequest).Name);
        var myType = request.GetType();
        
        IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
        
        foreach (var prop in props)
        {
            var propValue = prop.GetValue(request, null);
            logger.LogInformation("{Property} : {@Value}", prop.Name, propValue);
        }
        var response = await next();
        
        logger.LogInformation("Handled {ResponseName}", typeof(TRequest).Name);
        return response;
    }
}