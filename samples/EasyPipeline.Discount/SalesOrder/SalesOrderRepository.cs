namespace EasyPipeline.Discount;

public class SalesOrderRepository
{
    public Task<SalesOrder> CreateSalesOrder(SalesOrder salesOrder)
    {
        Console.WriteLine("Sales order created");
        return Task.FromResult(salesOrder);
    }
}
