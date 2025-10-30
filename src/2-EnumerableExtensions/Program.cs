// See https://aka.ms/new-console-template for more information
using Shared.Models;
using EnumerableExtensions;

Console.WriteLine("Testing Enumerable Extensions:");
Console.WriteLine("-----------------------------\n");

// Create sample orders
var orders = new List<Order>
{
    new()
    {
        Id = 1,
        CreatedAt = DateTime.Now.AddDays(-1),
        CreatedBy = "system",
        Status = "Pending",
        Items =
        [
            new OrderItem { ProductId = 1, ProductName = "Laptop", Quantity = 1, UnitPrice = 999.99m }
        ],
        TotalAmount = 999.99m
    },
    new()
    {
        Id = 2,
        CreatedAt = DateTime.Now.AddDays(-2),
        CreatedBy = "system",
        Status = "Completed",
        Items =
        [
            new OrderItem { ProductId = 2, ProductName = "Mouse", Quantity = 2, UnitPrice = 49.99m }
        ],
        TotalAmount = 99.98m
    }
};

Console.WriteLine("Processing with non-empty orders list:");
var orderService = new OrderService();
orderService.ProcessOrders(orders);

Console.WriteLine("\nProcessing with empty orders list:");
orderService.ProcessOrders(new List<Order>());

// Demonstrate additional cases
Console.WriteLine("\nProcessing with large order set:");
var largeOrderSet = Enumerable.Range(1, 175).Select(i => new Order
{
    Id = i,
    CreatedAt = DateTime.Now.AddDays(-i),
    CreatedBy = "system",
    Status = i % 2 == 0 ? "Completed" : "Pending",
    Items =
    [
        new OrderItem
        {
            ProductId = i,
            ProductName = $"Product {i}",
            Quantity = 1,
            UnitPrice = 100m
        }
    ],
    TotalAmount = 100m
});

orderService.ProcessOrders(largeOrderSet);
