namespace CSharp.Core.Models;

public class Order
{
    public string OrderId { get; set; } = "";
    public decimal Total { get; set; }
    public List<string> Items { get; set; } = [];
    public string Status { get; set; } = "Pending";
    public OrderValidationResult Validation { get; set; } = new();
    public OrderMetadata Metadata { get; set; } = new();

    public Order() { }

    public Order(string orderId, decimal total, List<string> items)
    {
        OrderId = orderId;
        Total = total;
        Items = items;
        Metadata = new OrderMetadata
        {
            CreatedAt = DateTime.UtcNow,
            CreatedBy = "system"
        };
    }
}