using Shared.Models;

namespace DomainExtensions;

public class AnalyticsService
{
    public void AnalyzeOrder(List<Order> orders)
    {
        var insights = orders
            .Where(o => o.DaysSinceOrder > 30)
            .Select(o => new
            {
                OrderId = o.Get("id"),        // Indexer-like accessor
                Category = o.RevenueCategory,      // Property
                Discount = o.CalculateDiscount(),  // Method
                Average = o.AverageItemValue       // Property
            })
            .OrderByDescending(x => x.Discount);   // operator overloads

        // Clean. Natural. Type-safe.
    }

    public void Analyze(List<Order> orders)
    {
        // Validate first — ensures only eligible orders are considered.
        var validOrders = orders
            .Where(o => o.ValidateForCheckout())
            .ToList();

        // Filter and project insights using all extension members.
        var insights = validOrders
            .Where(order => order.DaysSinceOrder > 30)
            .Select(order => new
            {
                OrderId = order.Get("id"),                // Indexer-like accessor
                RevenueCategory = order.RevenueCategory,  // Property (OrderAnalytics)
                Discount = order.CalculateDiscount(),     // Method (OrderOperations)
                AverageItemValue = order.AverageItemValue,// Property (OrderAnalytics)
                Status = order.Get("status"),             // Indexer-like accessor
                DaysOld = order.DaysSinceOrder
            })
            // Sort using property or the new comparison operators.
            .OrderByDescending(x => x.Discount)
            .ThenByDescending(x => x.AverageItemValue)
            .ToList();

        // Compare orders directly using the overloaded operators. 
        // Find the highest revenue order among all valid orders.
        if (validOrders.Count > 0)
        {
            // Use Aggregate with the overloaded > operator on Order.
            var highestRevenueOrder = validOrders.Aggregate((best, next) => next > best ? next : best);

            // Write a concise summary for the highest revenue order.
            Console.WriteLine("Highest revenue order:");
            Console.WriteLine(
                $"{highestRevenueOrder.Get("id"),-6} | {highestRevenueOrder.RevenueCategory,-12} | " +
                $"Discount: {highestRevenueOrder.CalculateDiscount(),6:C} | AvgItem: {highestRevenueOrder.AverageItemValue,6:C} | " +
                $"{highestRevenueOrder.Get("status"),-12} | {highestRevenueOrder.DaysSinceOrder,3} days old");
            Console.WriteLine(new string('-', 80));
        }

        // Display the analytics result.
        foreach (var i in insights)
        {
            Console.WriteLine(
                $"{i.OrderId,-6} | {i.RevenueCategory,-12} | " +
                $"Discount: {i.Discount,6:C} | AvgItem: {i.AverageItemValue,6:C} | " +
                $"{i.Status,-12} | {i.DaysOld,3} days old");
        }
    }
}
