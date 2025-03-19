
using EasyPipeline;
using EasyPipeline.Abstractions;
using EasyPipeline.Logging;
using EasyPipeline.Logging.SampleObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

Console.WriteLine("Hello, World!");

var services = new ServiceCollection();

services.AddLogging(configure => configure.AddConsole());
services.AddSingleton(typeof(PipelineExecuter<>));
services.AddTransient(typeof(IEasyPipeline<>), typeof(LoggingBehavior<>));

var service_provider = services.BuildServiceProvider();

var sample_obj_1 = new SampleObject1(10);
var sample_obj_2 = new SampleObject2(20);
var sample_obj_3 = new SampleObject3(30);

var pipelineExecuter = service_provider.GetRequiredService<PipelineExecuter<SampleObject1>>();
await pipelineExecuter.Run(sample_obj_1, CancellationToken.None);

var pipelineExecuter2 = service_provider.GetRequiredService<PipelineExecuter<SampleObject2>>();
await pipelineExecuter2.Run(sample_obj_2, CancellationToken.None);

var pipelineExecuter3 = service_provider.GetRequiredService<PipelineExecuter<SampleObject3>>();
await pipelineExecuter3.Run(sample_obj_3, CancellationToken.None);