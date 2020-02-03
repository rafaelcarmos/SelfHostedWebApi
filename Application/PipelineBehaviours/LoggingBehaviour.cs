using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application
{
    public class LoggingBehaviour<TRequest, TResponse> :  IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation($"Handling {typeof(TRequest).Name}");
            var response = await next();
            _logger.LogInformation($"Handled {typeof(TRequest).Name}");

            return response;
        }
    }
}
