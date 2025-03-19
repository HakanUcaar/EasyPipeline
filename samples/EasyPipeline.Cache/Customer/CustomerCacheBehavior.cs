using EasyPipeline.Abstractions;
using Microsoft.Extensions.Logging;
namespace EasyPipeline.Cache;

public class CustomerCacheBehavior<TIn,TOut>(ILogger<CustomerCacheBehavior<GetCustomerByIdQuery, Customer>> logger) : IEasyPipeline<GetCustomerByIdQuery, Customer>
{   

    public async Task<Customer> InvokeAsync(GetCustomerByIdQuery context, EasyPipelineDelegate<GetCustomerByIdQuery, Customer> next, CancellationToken cancellationToken)
    {
        if (CacheRepository.Cached_Customer is null)
        {
            CacheRepository.Cached_Customer = await next(context);
            logger.LogInformation("Data is cached");
        }

        logger.LogInformation("Cached data returned");
        return CacheRepository.Cached_Customer;
    }
}

