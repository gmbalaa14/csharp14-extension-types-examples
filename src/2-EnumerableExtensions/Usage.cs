using Shared.Models;

namespace EnumerableExtensions;

public class OrderService
{
    public void ProcessOrders(IEnumerable<Order> orders)
    {
        // Use extension properties
        if (orders.IsEmpty)
        {
            Console.WriteLine("No orders to process");
            return;
        }

        Console.WriteLine($"Total orders: {orders.TotalCount}");

        // Pagination
        var pagedOrders = orders.ToPagedResult(pageNumber: 1, pageSize: 10);
        Console.WriteLine($"Page {pagedOrders.PageNumber} of {pagedOrders.TotalPages}");
        Console.WriteLine($"Has next page: {pagedOrders.HasNextPage}");

        // Batch processing
        foreach (var batch in orders.Batch(50))
        {
            ProcessBatch(batch);
        }
    }

    private void ProcessBatch(IEnumerable<Order> batch)
    {
        // Process batch logic
    }
}
