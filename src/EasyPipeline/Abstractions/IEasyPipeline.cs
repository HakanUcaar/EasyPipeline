namespace EasyPipeline.Abstractions;

public delegate Task EasyPipelineDelegate<in TIn>(TIn context, CancellationToken cancellationToken = default);

public interface IEasyPipeline<TIn>
{
    Task InvokeAsync(TIn context, EasyPipelineDelegate<TIn> next, CancellationToken cancellationToken);
}

public delegate Task<TOut> EasyPipelineDelegate<TIn, TOut>(TIn context, CancellationToken cancellationToken = default);

public interface IEasyPipeline<TIn, TOut> 
{
    Task<TOut> InvokeAsync(TIn context, EasyPipelineDelegate<TIn,TOut> next, CancellationToken cancellationToken);
}