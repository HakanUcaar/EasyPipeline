using EasyPipeline.Abstractions;
using EasyPipeline.Samples.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace EasyPipeline.Samples;

public static class ServiceRegisters
{
    public static IServiceCollection CustomerPiplineRegister(this IServiceCollection services)
    {
        services.AddTransient(typeof(IEasyPipeline<>), typeof(CustomerValidationBehavior<>));

        return services;
    }

    public static IServiceCollection SalesOrderPipelineRegister(this IServiceCollection services)
    {
        services.AddTransient(typeof(IEasyPipeline<>), typeof(SalesOrderGrossTotalAggregateBehavior<>));

        return services;
    }

    public static IServiceCollection LogPipelineRegister(this IServiceCollection services)
    {
        services.AddTransient(typeof(IEasyPipeline<>), typeof(LoggingBehavior<>));

        return services;
    }
}
