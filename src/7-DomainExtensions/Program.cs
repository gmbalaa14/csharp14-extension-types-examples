// See https://aka.ms/new-console-template for more information
using DomainExtensions;
using Shared.Models;

// Create sample orders
var orders = new List<Order>
{
    new()
    {
        Id = 1,
        CreatedAt = DateTime.Now.AddDays(-45), // 45 days old
        CreatedBy = "system",
        Status = "Completed",
        Items =
        [
            new OrderItem
            {
                ProductId = 1,
                ProductName = "Laptop",
                Quantity = 2,
                UnitPrice = 999.99m
            },
            new OrderItem
            {
                ProductId = 2,
                ProductName = "Mouse",
                Quantity = 3,
                UnitPrice = 29.99m
            }
        ],
        TotalAmount = 2089.95m
    },
    new()
    {
        Id = 2,
        CreatedAt = DateTime.Now.AddDays(-15), // 15 days old
        CreatedBy = "system",
        Status = "Completed",
        Items =
        [
            new OrderItem
            {
                ProductId = 3,
                ProductName = "Monitor",
                Quantity = 1,
                UnitPrice = 399.99m
            }
        ],
        TotalAmount = 399.99m
    },
    // New orders
    new()
    {
        Id = 3,
        CreatedAt = DateTime.Now.AddDays(-60), // 60 days old
        CreatedBy = "system",
        Status = "Completed",
        Items =
        [
            new OrderItem
            {
                ProductId = 4,
                ProductName = "Gaming Chair",
                Quantity = 2,
                UnitPrice = 299.99m
            },
            new OrderItem
            {
                ProductId = 5,
                ProductName = "Mechanical Keyboard",
                Quantity = 1,
                UnitPrice = 159.99m
            }
        ],
        TotalAmount = 759.97m
    },
    new()
    {
        Id = 4,
        CreatedAt = DateTime.Now.AddDays(-35), // 35 days old
        CreatedBy = "system",
        Status = "Pending",
        Items =
        [
            new OrderItem
            {
                ProductId = 6,
                ProductName = "Graphics Card",
                Quantity = 1,
                UnitPrice = 899.99m
            }
        ],
        TotalAmount = 899.99m
    },
    new()
    {
        Id = 5,
        CreatedAt = DateTime.Now.AddDays(-90), // 90 days old
        CreatedBy = "system",
        Status = "Completed",
        Items =
        [
            new OrderItem
            {
                ProductId = 7,
                ProductName = "SSD 1TB",
                Quantity = 3,
                UnitPrice = 129.99m
            },
            new OrderItem
            {
                ProductId = 8,
                ProductName = "RAM 32GB",
                Quantity = 2,
                UnitPrice = 149.99m
            }
        ],
        TotalAmount = 689.95m
    }
};

Console.WriteLine("Running Order Analytics...\n");

var analyticsService = new AnalyticsService();

Console.WriteLine("Full Analysis:");
Console.WriteLine("==============");
analyticsService.Analyze(orders);
