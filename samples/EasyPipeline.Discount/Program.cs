
using EasyPipeline.Abstractions;
using EasyPipeline.Discount;
using EasyPipeline;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

Console.WriteLine("Hello, World!");

var services = new ServiceCollection();

services.AddLogging(configure => configure.AddConsole());
services.AddSingleton(typeof(PipelineExecuter<,>));
services.AddSingleton<SalesOrderRepository>();
services.AddSingleton<CreateSalesOrder>();
services.AddScoped<CreateSalesOrderCommand>();
services.AddTransient(typeof(IEasyPipeline<,>), typeof(SalesOrderDiscountBehavior<,>));

var service_provider = services.BuildServiceProvider();

var createSales = service_provider.GetRequiredService<CreateSalesOrder>();

var command = new CreateSalesOrderCommand(
        new SalesOrder("1", "Osman Pazarlama", "Bir yerler", new() { new("Bilgisayar", 3, 1500, 20), new("Klavye", 2, 350, 10) })
    );

await createSales.Handle(command);