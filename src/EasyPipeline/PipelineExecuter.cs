using EasyPipeline.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace EasyPipeline
{
    public class PipelineExecuter<TContext>(IServiceProvider serviceProvider) where TContext : class
    {
        public async Task Run(TContext context, CancellationToken cancellationToken) 
        {
            var finalProcess = new EasyPipelineDelegate<TContext>(async (context, cancellationToken) =>
            {
                await Task.FromResult(true);
            });

            var pipeline = serviceProvider
                .GetServices<IEasyPipeline<TContext>>()
                .Reverse()
                .Aggregate(
                    finalProcess,
                    (next, pipeline) => async (context, cancellationToken) =>
                    {
                        try
                        {
                            await pipeline.InvokeAsync(context, next, cancellationToken);
                        }
                        catch (Exception ex)
                        {
                            // TODO : Add ExceptionHandler
                            // TODO : Add ILogger 
                            Console.WriteLine("Error in pipeline:{0}", ex.Message);                           
                        }
                    });

            await pipeline(context, cancellationToken);
        }
    }
}
