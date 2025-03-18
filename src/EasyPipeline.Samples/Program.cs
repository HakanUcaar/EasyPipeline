using EasyPipeline;
using EasyPipeline.Samples;
using EasyPipeline.Samples.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

Console.WriteLine("Hello, World!");

var services = new ServiceCollection();
services.AddLogging(configure => configure.AddConsole());

services
    .CustomerPiplineRegister()
    .SalesOrderPipelineRegister()
    .LogPipelineRegister();

var service_provider = services.BuildServiceProvider();


// -- Sample 1 -- //
var customer = new Customer("Hakan", "Uçar", string.Empty, "turkiye");
var pipelineExecuter = new PipelineExecuter<Customer>(service_provider);
await pipelineExecuter.Run(customer, CancellationToken.None);

// -- Sample 2 -- //
var sales_order = new SalesOrder("1", "Osman Pazarlama", "Bir yerler", new() { new("Bilgisayar", 3, 1500), new("Klavye", 2, 350) });
var pipelineExecuter2 = new PipelineExecuter<SalesOrder>(service_provider);
await pipelineExecuter2.Run(sales_order, CancellationToken.None);