using EasyPipeline.Abstractions;
using EasyPipeline.Notification.UseCases;
using Microsoft.Extensions.Logging;

namespace EasyPipeline.Notification;

public class SalesOrderNotificationBehavior<TIn, TOut>(ILogger<SalesOrderNotificationBehavior<CreateSalesOrderCommand, bool>> logger) : IEasyPipeline<CreateSalesOrderCommand, bool>
{
    public async Task<bool> InvokeAsync(CreateSalesOrderCommand context, EasyPipelineDelegate<CreateSalesOrderCommand, bool> next, CancellationToken cancellationToken)
    {
        var result = await next(context);

        // -- send notification --//
        logger.LogInformation("Sales order notification event created");

        return result;
    }
}