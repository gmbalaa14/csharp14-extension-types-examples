namespace Shared.Models;

public class Order : IEntity, IAuditable, ISoftDeletable
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }

    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = null!;

    public List<OrderItem> Items { get; set; } = [];
}

public class OrderItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public bool IsValid =>
        ProductId > 0 &&
        !string.IsNullOrWhiteSpace(ProductName) &&
        Quantity > 0 &&
        UnitPrice > 0;
}
