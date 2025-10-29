using Shared.Models;

namespace DomainExtensions;

public static partial class OrderExtensions
{
    // Properties and categorization logic for analytics
    extension(Order order)
    {
        public decimal AverageItemValue =>
            order.Items.Count > 0
                ? order.TotalAmount / order.Items.Count
                : 0;

        public int DaysSinceOrder =>
            (DateTime.Now - order.CreatedAt).Days;

        public string RevenueCategory =>
            order.TotalAmount switch
            {
                < 100 => "Low Value",
                < 500 => "Medium Value",
                _ => "High Value"
            };

        public bool IsAtRisk =>
            order.DaysSinceOrder > 30 && order.TotalAmount > 500;
    }
}

public static partial class OrderExtensions
{
    // Methods extending Order for business operations
    extension(Order order)
    {
        public decimal CalculateDiscount() =>
            order.TotalAmount switch
            {
                >= 1000 => order.TotalAmount * 0.15m,
                >= 500 => order.TotalAmount * 0.10m,
                >= 100 => order.TotalAmount * 0.05m,
                _ => 0
            };

        public bool ValidateForCheckout() =>
            order.Items is { Count: > 0 } &&
            order.TotalAmount > 0 &&
            order.Items.All(i => i.IsValid);
    }
}

public static partial class OrderExtensions
{
    // Indexer-like accessor for dynamic property retrieval
    extension(Order order)
    {
        public string Get(string field) =>
            field.ToLower() switch
            {
                "id" => $"#{order.Id}",
                "revenue" => $"${order.TotalAmount:N2}",
                "items" => $"{order.Items.Count} items",
                "date" => order.CreatedAt.ToString("MMM dd, yyyy"),
                "status" => order.TotalAmount > 500
                    ? "High Value"
                    : "Standard",
                _ => "N/A"
            };
    }
}

public static partial class OrderExtensions
{
    // Overloaded comparison operators for Order
    extension(Order order)
    {
        public static bool operator >(Order left, Order right) =>
            left.TotalAmount > right.TotalAmount;

        public static bool operator <(Order left, Order right) =>
            left.TotalAmount < right.TotalAmount;

        public static bool operator >=(Order left, Order right) =>
            left.TotalAmount >= right.TotalAmount;

        public static bool operator <=(Order left, Order right) =>
            left.TotalAmount <= right.TotalAmount;

        public static bool operator ==(Order left, Order right) =>
            left.TotalAmount == right.TotalAmount;

        public static bool operator !=(Order left, Order right) =>
            left.TotalAmount != right.TotalAmount;
    }
}

