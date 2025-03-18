# EasyPipeline
Basit obje bazlı pipeline oluşturma konsept

## Örnek 1

### Customer classına validation ekleme

Customer classı :

``` csharp
public record Customer(string Name, string LastName, string Tel, string Country);
```

Behavior oluşturma :

``` csharp
public class CustomerValidationBehavior<TContract>(ILogger<CustomerValidationBehavior<TContract>> logger)
        : IEasyPipeline<TContract> where TContract : Customer
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
```

Kullanım : 

``` csharp
services.AddTransient(typeof(IEasyPipeline<>), typeof(CustomerValidationBehavior<>));

var customer = new Customer("Hakan", "Uçar", string.Empty, "turkiye");
var pipelineExecuter = new PipelineExecuter<Customer>(service_provider);
await pipelineExecuter.Run(customer, CancellationToken.None);
```

Output :

![image](https://github.com/user-attachments/assets/20526517-7117-4166-8ffc-f4a716e584d1)

## Örnek 2

### Tip bağımsız log ekleme behavior oluşturma :

``` csharp
public class LoggingBehavior<TContract>(ILogger<LoggingBehavior<TContract>> logger) : IEasyPipeline<TContract> where TContract : class
{
    public async Task InvokeAsync(TContract context, EasyPipelineDelegate<TContract> next, CancellationToken cancellationToken)
    {
        logger.LogInformation($"[LOG] {JsonSerializer.Serialize(context, new JsonSerializerOptions
        {
            WriteIndented = true
        })}");

        await next(context);
    }
}
```

Kullanım : 

``` csharp
services.AddTransient(typeof(IEasyPipeline<>), typeof(LoggingBehavior<>));

var customer = new Customer("Hakan", "Uçar", string.Empty, "turkiye");
var pipelineExecuter = new PipelineExecuter<Customer>(service_provider);
await pipelineExecuter.Run(customer, CancellationToken.None);
```

Output :

![image](https://github.com/user-attachments/assets/d5f212e7-88af-480b-8a85-253993f63ba0)


## Örnek 3

### Field maniplasyonu :

SalesOrder classı :

``` csharp
public record SalesOrder(string CustemerNo, string CustomerName, string Address, List<SalesOrderItem> Items)
{
    public decimal GrossTotal { get; set; }
};

public record SalesOrderItem(string ItemName, double Quantity, decimal UnitPrice);
```

Behavior oluşturma :

``` csharp
public class SalesOrderGrossTotalAggregateBehavior<TContract>(ILogger<SalesOrderGrossTotalAggregateBehavior<TContract>> logger)
        : IEasyPipeline<TContract> where TContract : SalesOrder
{
    public async Task InvokeAsync(TContract context, EasyPipelineDelegate<TContract> next, CancellationToken cancellationToken)
    {

        context.GrossTotal = context.Items.Sum(x => (decimal)x.Quantity * x.UnitPrice);
        logger.LogInformation($"[AGGREGATE] GrossTotal");

        await next(context);
    }
}
```

Kullanım : 

``` csharp
services.AddTransient(typeof(IEasyPipeline<>), typeof(SalesOrderGrossTotalAggregateBehavior<>));

var sales_order = new SalesOrder("1", "Osman Pazarlama", "Bir yerler", new() { new("Bilgisayar", 3, 1500), new("Klavye", 2, 350) });
var pipelineExecuter2 = new PipelineExecuter<SalesOrder>(service_provider);
await pipelineExecuter2.Run(sales_order, CancellationToken.None);
```

Output :

![image](https://github.com/user-attachments/assets/7b0eaa2e-58d0-4064-b140-b0637a9604a9)



