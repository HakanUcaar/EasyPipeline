// See https://aka.ms/new-console-template for more information
using EasyPipeline.Abstractions;
using EasyPipeline;
using Microsoft.Extensions.DependencyInjection;
using EasyPipeline.Notification;
using EasyPipeline.Notification.UseCases;
using Microsoft.Extensions.Logging;

Console.WriteLine("Hello, World!");

var services = new ServiceCollection();

services.AddLogging(configure => configure.AddConsole());
services.AddSingleton(typeof(PipelineExecuter<,>));
services.AddSingleton<SalesOrderRepository>();
services.AddSingleton<CreateSalesOrder>();
services.AddScoped<CreateSalesOrderCommand>();
services.AddTransient(typeof(IEasyPipeline<,>), typeof(SalesOrderNotificationBehavior<,>));

var service_provider = services.BuildServiceProvider();

var createSales = service_provider.GetRequiredService<CreateSalesOrder>();

var command = new CreateSalesOrderCommand(
        new SalesOrder("1", "Osman Pazarlama", "Bir yerler",5200, new() { new("Bilgisayar", 3, 1500), new("Klavye", 2, 350) })
    );

await createSales.Handle(command);