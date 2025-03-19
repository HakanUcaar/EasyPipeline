using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace EasyPipeline.Discount;


public record CreateSalesOrderCommand(SalesOrder salesOrder);
public class CreateSalesOrder(PipelineExecuter<CreateSalesOrderCommand, SalesOrder> pipelineExecuter, SalesOrderRepository salesOrderRepository, ILogger<CreateSalesOrder> logger)
{
    public async Task<bool> Handle(CreateSalesOrderCommand command)
    {
        var salesOrder = await pipelineExecuter.Run(command, (command) =>
        {
            Console.WriteLine("Method execute");
            return salesOrderRepository.CreateSalesOrder(command.salesOrder).GetAwaiter().GetResult();
        });

        logger.LogInformation($"[LOG] {JsonSerializer.Serialize(salesOrder, new JsonSerializerOptions
        {
            WriteIndented = true
        })}");

        return true;
    }
}
