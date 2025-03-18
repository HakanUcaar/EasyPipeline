namespace EasyPipeline.Abstractions;

public interface IEasyContext
{

}

public interface IEasyContext<out T> : IEasyContext where T : class
{
    T Message { get; }
}
