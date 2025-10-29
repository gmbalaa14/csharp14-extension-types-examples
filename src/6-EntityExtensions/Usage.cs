using Shared.Models;

namespace EntityExtensions;

public class OrderService
{
    public void ManageOrders()
    {
        var order = new Order
        {
            TotalAmount = 100m,
            Status = "Pending"
        };

        // Use extension methods
        order.MarkAsCreated("user123");

        Console.WriteLine($"Is New: {order.IsNew}");
        Console.WriteLine($"Days Since Created: {order.DaysSinceCreated}");

        // Validate
        if (order.Validate(out var errors))
        {
            Console.WriteLine("Order is valid");
        }
        else
        {
            Console.WriteLine($"Validation errors: {string.Join(", ", errors)}");
        }

        // Update order
        order.Status = "Processing";
        order.MarkAsUpdated("user123");

        // Soft delete
        order.MarkAsDeleted("admin456");

        // Collection operations
        var orders = new List<Order>
        {
            order,
            new Order().MarkAsCreated("user456"),
            new Order().MarkAsCreated("user789")
        };

        var recentOrders = orders.CreatedInLast(7);
        var activeOrders = orders.Active();
        var stats = orders.GetStatistics();

        foreach (var stat in stats)
        {
            Console.WriteLine($"{stat.Key}: {stat.Value}");
        }
    }
}
