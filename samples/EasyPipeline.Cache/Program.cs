// See https://aka.ms/new-console-template for more information
using EasyPipeline;
using EasyPipeline.Abstractions;
using EasyPipeline.Cache;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

Console.WriteLine("Hello, World!");

var services = new ServiceCollection();

services.AddLogging(configure => configure.AddConsole());
services.AddSingleton(typeof(PipelineExecuter<,>));
services.AddSingleton<CustomerRepository>();
services.AddScoped<GetCustomerById>();
services.AddTransient(typeof(IEasyPipeline<,>), typeof(CustomerCacheBehavior<,>));

var service_provider = services.BuildServiceProvider();

var getCustomerById = service_provider.GetRequiredService<GetCustomerById>();

await getCustomerById.Handle(new(1));
await getCustomerById.Handle(new(1));
await getCustomerById.Handle(new(1));
await getCustomerById.Handle(new(1));
await getCustomerById.Handle(new(1));