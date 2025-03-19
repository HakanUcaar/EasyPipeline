using EasyPipeline;
using EasyPipeline.Abstractions;
using EasyPipeline.Aggregate;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

Console.WriteLine("Hello, World!");

var services = new ServiceCollection();

services.AddLogging(configure => configure.AddConsole());
services.AddSingleton(typeof(PipelineExecuter<>));
services.AddTransient(typeof(IEasyPipeline<>), typeof(SalesOrderGrossTotalAggregateBehavior<>));

var service_provider = services.BuildServiceProvider();

var logger = service_provider.GetRequiredService<ILogger<SalesOrder>>();

var sales_order = new SalesOrder("1", "Osman Pazarlama", "Bir yerler", new() { new("Bilgisayar", 3, 1500), new("Klavye", 2, 350) });

logger.LogInformation($"[LOG] GrossTotal Before : {sales_order.GrossTotal}");
var pipelineExecuter = service_provider.GetRequiredService<PipelineExecuter<SalesOrder>>();
await pipelineExecuter.Run(sales_order, CancellationToken.None);
logger.LogInformation($"[LOG] GrossTotal After : {sales_order.GrossTotal}");


