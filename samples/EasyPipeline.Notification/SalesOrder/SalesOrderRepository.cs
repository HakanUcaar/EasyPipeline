namespace EasyPipeline.Notification;

public class SalesOrderRepository
{
    public Task<bool> CreateSalesOrder(SalesOrder salesOrder)
    {
        Console.WriteLine("Sales order created");
        return Task.FromResult(true);
    }
}
