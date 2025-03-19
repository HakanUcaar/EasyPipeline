namespace EasyPipeline.Abstractions;

public delegate Task EasyPipelineDelegate<in TIn>(TIn context, CancellationToken cancellationToken = default) where TIn : class;

public interface IEasyPipeline<TIn> where TIn : class
{
    Task InvokeAsync(TIn context, EasyPipelineDelegate<TIn> next, CancellationToken cancellationToken);
}

public delegate Task<TOut> EasyPipelineDelegate<TIn, TOut>(TIn context, CancellationToken cancellationToken = default) where TIn : class where TOut : class;

public interface IEasyPipeline<TIn, TOut> where TIn : class where TOut : class
{
    Task<TOut> InvokeAsync(TIn context, EasyPipelineDelegate<TIn,TOut> next, CancellationToken cancellationToken);
}