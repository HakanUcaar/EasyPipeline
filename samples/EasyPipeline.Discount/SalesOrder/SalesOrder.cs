namespace EasyPipeline.Discount;

public record SalesOrder(string CustemerNo, string CustomerName, string Address, List<SalesOrderItem> Items)
{
    public decimal GrossTotal { get; set; }
};

public record SalesOrderItem(string ItemName, double Quantity, decimal UnitPrice, int Discount)
{
    public decimal ItemTotal { get; set; }
}
