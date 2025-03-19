using EasyPipeline.Abstractions;
using Microsoft.Extensions.Logging;

namespace EasyPipeline.Validate;

public class CustomerValidationBehavior<TContract>(ILogger<CustomerValidationBehavior<Customer>> logger) : IEasyPipeline<Customer>
{
    public async Task InvokeAsync(Customer context, EasyPipelineDelegate<Customer> next, CancellationToken cancellationToken)
    {
        if(context == null || string.IsNullOrEmpty(context.Tel))
        {
            logger.LogError("[VALIDATION] context.Name is null or empty");
            //throw new ArgumentNullException(nameof(context.Name));
        }        

        await next(context);
    }
}
