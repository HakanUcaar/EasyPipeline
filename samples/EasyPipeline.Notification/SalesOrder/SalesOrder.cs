namespace EasyPipeline.Notification;

public record SalesOrder(string CustemerNo, string CustomerName, string Address, decimal GrossTotal, List<SalesOrderItem> Items);

public record SalesOrderItem(string ItemName, double Quantity, decimal UnitPrice);
