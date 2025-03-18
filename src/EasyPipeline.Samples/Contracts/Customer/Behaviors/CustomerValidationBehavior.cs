﻿using EasyPipeline.Abstractions;
using EasyPipeline.Samples.Contracts;
using Microsoft.Extensions.Logging;

namespace EasyPipeline.Samples.Behaviors;

public class CustomerValidationBehavior<TContract>(ILogger<CustomerValidationBehavior<TContract>> logger) : IEasyPipeline<TContract> where TContract : Customer
{
    public async Task InvokeAsync(TContract context, EasyPipelineDelegate<TContract> next, CancellationToken cancellationToken)
    {
        if(context == null || string.IsNullOrEmpty(context.Tel))
        {
            logger.LogError("[VALIDATION] context.Name is null or empty");
            //throw new ArgumentNullException(nameof(context.Name));
        }        

        await next(context);
    }
}
