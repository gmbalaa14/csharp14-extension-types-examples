// See https://aka.ms/new-console-template for more information
using QueryableExtensions;
using Shared.Models;

var querableProducts = new List<Product>
{
    new() { Id = 1, Name = "Laptop", Price = 1200.00m, Category = "Electronics" },
    new() { Id = 2, Name = "Smartphone", Price = 800.00m, Category = "Electronics" },
    new() { Id = 3, Name = "Desk Chair", Price = 150.00m, Category = "Furniture" },
    new() { Id = 4, Name = "Bookcase", Price = 200.00m, Category = "Furniture" },
    new() { Id = 5, Name = "Headphones", Price = 100.00m, Category = "Electronics" },
    new() { Id = 6, Name = "Coffee Table", Price = 250.00m, Category = "Furniture" },
    new() { Id = 7, Name = "Monitor", Price = 300.00m, Category = "Electronics" },
    new() { Id = 8, Name = "Keyboard", Price = 80.00m, Category = "Electronics" },
    new() { Id = 9, Name = "Mouse", Price = 50.00m, Category = "Electronics" },
    new() { Id = 10, Name = "Office Desk", Price = 400.00m, Category = "Furniture" },
    new() { Id = 11, Name = "Tablet", Price = 600.00m, Category = "Electronics" },
    new() { Id = 12, Name = "Printer", Price = 150.00m, Category = "Electronics" },
    new() { Id = 13, Name = "Router", Price = 120.00m, Category = "Electronics" },
    new() { Id = 14, Name = "Webcam", Price = 90.00m, Category = "Electronics" },
    new() { Id = 15, Name = "Speakers", Price = 110.00m, Category = "Electronics" }
}.AsQueryable();

var productRepository = new ProductRepository(querableProducts);
var pagedResult = productRepository.GetProducts("Electronics");

Console.WriteLine("Paged Electronics Products:");
Console.WriteLine($"Total Electronics Products: {pagedResult.TotalCount}");
Console.WriteLine($"Total Pages: {pagedResult.TotalPages}");
Console.WriteLine($"Products on Page {pagedResult.PageNumber}:");
foreach (var product in pagedResult.Items)
{
    Console.WriteLine($"- {product.Name} (${product.Price})");
}

Console.WriteLine("-----------------------------------");
Console.WriteLine("Getting product by ID 3:");
var returnedProduct = await productRepository.GetProductByIdAsync(3);
Console.WriteLine(returnedProduct == null ? "Product not found." : $"Product found: {returnedProduct.Name}");
