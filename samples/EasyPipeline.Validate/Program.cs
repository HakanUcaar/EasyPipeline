using EasyPipeline;
using EasyPipeline.Abstractions;
using EasyPipeline.Validate;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

Console.WriteLine("Hello, World!");

var services = new ServiceCollection();

services.AddLogging(configure => configure.AddConsole());
services.AddSingleton(typeof(PipelineExecuter<>));
services.AddTransient(typeof(IEasyPipeline<>), typeof(CustomerValidationBehavior<>));

var service_provider = services.BuildServiceProvider();

var customer = new Customer(1, "Hakan", "Uçar", string.Empty, "turkiye");
var pipelineExecuter = service_provider.GetRequiredService<PipelineExecuter<Customer>>();
await pipelineExecuter.Run(customer, CancellationToken.None);


