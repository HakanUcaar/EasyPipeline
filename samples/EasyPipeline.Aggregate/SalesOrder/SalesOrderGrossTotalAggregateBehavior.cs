using EasyPipeline.Abstractions;
using Microsoft.Extensions.Logging;

namespace EasyPipeline.Aggregate;

public class SalesOrderGrossTotalAggregateBehavior<TContract>(ILogger<SalesOrderGrossTotalAggregateBehavior<TContract>> logger) : IEasyPipeline<TContract> where TContract : SalesOrder
{
    public async Task InvokeAsync(TContract context, EasyPipelineDelegate<TContract> next, CancellationToken cancellationToken)
    {

        context.GrossTotal = context.Items.Sum(x => (decimal)x.Quantity * x.UnitPrice);
        logger.LogInformation($"[SUM] GrossTotal");

        await next(context);
    }
}