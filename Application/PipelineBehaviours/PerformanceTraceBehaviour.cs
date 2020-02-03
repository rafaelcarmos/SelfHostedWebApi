using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application
{
    public class PerformanceTraceBehaviour<TRequest, TResponse> :  IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<PerformanceTraceBehaviour<TRequest, TResponse>> _logger;

        public PerformanceTraceBehaviour(ILogger<PerformanceTraceBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var sw = Stopwatch.StartNew();
            var response = await next();
            sw.Stop();
            _logger.LogInformation($"Request {typeof(TRequest).Name} - Time elapsed: {sw.ElapsedMilliseconds}");

            return response;
        }
    }
}
