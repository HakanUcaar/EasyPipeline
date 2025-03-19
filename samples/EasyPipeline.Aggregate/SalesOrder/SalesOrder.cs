namespace EasyPipeline.Aggregate;

public record SalesOrder(string CustemerNo, string CustomerName, string Address, List<SalesOrderItem> Items)
{
    public decimal GrossTotal { get; set; }
};

public record SalesOrderItem(string ItemName, double Quantity, decimal UnitPrice);
