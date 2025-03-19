using EasyPipeline.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace EasyPipeline
{
    public class PipelineExecuter<TContext>(IServiceProvider serviceProvider) where TContext : class
    {
        public async Task Run(TContext context, CancellationToken cancellationToken = default)
        {
            await Run(context, (context) => { }, cancellationToken);
        }

        public async Task<TContext> Run(TContext context, Action<TContext> action, CancellationToken cancellationToken)
        {
            var finalProcess = new EasyPipelineDelegate<TContext>((context, cancellationToken) =>
            {
                action(context);
                return Task.CompletedTask;
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

            return context;
        }
    }

    public class PipelineExecuter<TIn, TOut>(IServiceProvider serviceProvider)
    {
        public async Task<TOut?> Run(TIn context, CancellationToken cancellationToken = default)
        {
            return await Run(context, (context) => default(TOut), cancellationToken);
        }

        public async Task<TOut?> Run(TIn context, Func<TIn, TOut> action, CancellationToken cancellationToken = default)
        {
            var finalProcess = new EasyPipelineDelegate<TIn, TOut>((context, cancellationToken) =>
            {
                return Task.FromResult(action(context));
            });

            var pipeline = serviceProvider
                .GetServices<IEasyPipeline<TIn, TOut>>()
                .Reverse()
                .Aggregate(
                    finalProcess,
                    (next, pipeline) => async (context, cancellationToken) =>
                    {
                        try
                        {
                            return await pipeline.InvokeAsync(context, next, cancellationToken);
                        }
                        catch (Exception ex)
                        {
                            // TODO : Add ExceptionHandler
                            // TODO : Add ILogger 
                            Console.WriteLine("Error in pipeline:{0}", ex.Message);
                            return default(TOut);
                        }
                    });

            return await pipeline(context, cancellationToken);
        }
    }
}
