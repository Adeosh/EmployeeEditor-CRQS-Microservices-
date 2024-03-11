using MediatR;
using Microsoft.Extensions.Logging;

namespace EmployeeEditor.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> 
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(
            ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request, 
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogInformation("Starting request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

            var result = await next();

            _logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", requestName, DateTime.UtcNow);

            if (result is null)
            {
                _logger.LogError("Request failure {@RequestName}, {@DateTimeUtc}",
                    requestName, DateTime.UtcNow);
            }

            return result;
        }
    }
}
