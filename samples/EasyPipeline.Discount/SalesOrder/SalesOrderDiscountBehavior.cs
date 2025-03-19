using EasyPipeline.Abstractions;
using Microsoft.Extensions.Logging;

namespace EasyPipeline.Discount;

public class SalesOrderDiscountBehavior<TIn, TOut>(ILogger<SalesOrderDiscountBehavior<CreateSalesOrderCommand, SalesOrder>> logger) : IEasyPipeline<CreateSalesOrderCommand, SalesOrder>
{
    public async Task<SalesOrder> InvokeAsync(CreateSalesOrderCommand context, EasyPipelineDelegate<CreateSalesOrderCommand, SalesOrder> next, CancellationToken cancellationToken)
    {

        var salesOrderMutation = context.salesOrder;

        salesOrderMutation.GrossTotal = salesOrderMutation.Items.Sum(x => x.UnitPrice * (decimal)x.Quantity *(1-((decimal)x.Discount)/100));
        salesOrderMutation.Items.ForEach(x => x.ItemTotal = x.UnitPrice * (decimal)x.Quantity * (1-((decimal)x.Discount / 100)));

        logger.LogInformation("Discount applied");

        return await next(context);
    }
}