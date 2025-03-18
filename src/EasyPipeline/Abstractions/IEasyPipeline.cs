namespace EasyPipeline.Abstractions;

public delegate Task EasyPipelineDelegate<TContext>(
    TContext context,
    CancellationToken cancellationToken = default
) where TContext : class;

public interface IEasyPipeline<TContext> where TContext : class
{
    Task InvokeAsync(TContext context, EasyPipelineDelegate<TContext> next, CancellationToken cancellationToken);
}