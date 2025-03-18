using EasyPipeline.Abstractions;
using EasyPipeline.Samples.Contracts;
using Microsoft.Extensions.Logging;

namespace EasyPipeline.Samples.Behaviors;

public class SalesOrderGrossTotalAggregateBehavior<TContract>(ILogger<SalesOrderGrossTotalAggregateBehavior<TContract>> logger) : IEasyPipeline<TContract> where TContract : SalesOrder
{
    public async Task InvokeAsync(TContract context, EasyPipelineDelegate<TContract> next, CancellationToken cancellationToken)
    {

        context.GrossTotal = context.Items.Sum(x => (decimal)x.Quantity * x.UnitPrice);
        logger.LogInformation($"[AGGREGATE] GrossTotal");

        await next(context);
    }
}