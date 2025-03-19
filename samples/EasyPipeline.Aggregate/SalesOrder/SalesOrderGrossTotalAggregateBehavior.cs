using EasyPipeline.Abstractions;
using Microsoft.Extensions.Logging;

namespace EasyPipeline.Aggregate;

public class SalesOrderGrossTotalAggregateBehavior<TContract>(ILogger<SalesOrderGrossTotalAggregateBehavior<SalesOrder>> logger) : IEasyPipeline<SalesOrder> 
{
    public async Task InvokeAsync(SalesOrder context, EasyPipelineDelegate<SalesOrder> next, CancellationToken cancellationToken)
    {

        context.GrossTotal = context.Items.Sum(x => (decimal)x.Quantity * x.UnitPrice);
        logger.LogInformation($"[SUM] GrossTotal");

        await next(context);
    }
}