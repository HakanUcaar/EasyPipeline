using EasyPipeline.Abstractions;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace EasyPipeline.Logging;

public class LoggingBehavior<TContract>(ILogger<LoggingBehavior<TContract>> logger) : IEasyPipeline<TContract> where TContract : class
{
    public async Task InvokeAsync(TContract context, EasyPipelineDelegate<TContract> next, CancellationToken cancellationToken)
    {
        logger.LogInformation($"[LOG] {JsonSerializer.Serialize(context, new JsonSerializerOptions
        {
            WriteIndented = true
        })}");

        await next(context);
    }
}