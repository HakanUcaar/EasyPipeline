namespace EasyPipeline.Notification.UseCases;


public record CreateSalesOrderCommand(SalesOrder salesOrder);
public class CreateSalesOrder(PipelineExecuter<CreateSalesOrderCommand, bool> pipelineExecuter, SalesOrderRepository salesOrderRepository)
{
    public Task<bool> Handle(CreateSalesOrderCommand command)
    {
        return pipelineExecuter.Run(command, (command) =>
        {
            Console.WriteLine("Method execute");
            return salesOrderRepository.CreateSalesOrder(command.salesOrder).GetAwaiter().GetResult();
        });
    }
}
