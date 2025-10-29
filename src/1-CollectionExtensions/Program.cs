// See https://aka.ms/new-console-template for more information
using CollectionExtensions;

Console.WriteLine("Sales Data Analytics Demo");
Console.WriteLine("=======================\n");

var analyticsDemo = new AnalyticsDemo();
analyticsDemo.AnalyzeSalesData();

// Additional analytics demonstrations
Console.WriteLine("\nExtended Sales Analysis:");
Console.WriteLine("----------------------");

var monthlySales = new List<decimal>
{
    1250.50m, 875.75m, 2100.00m, 950.25m,
    1475.00m, 3500.00m, 1100.00m, 925.50m,
    1600.00m, 750.25m, 2250.75m, 1800.00m
};

// Basic statistics
Console.WriteLine("\nMonthly Sales Statistics:");
Console.WriteLine($"Median Sale: {monthlySales.Median:C}");

// Detailed statistics
Console.WriteLine("\nDetailed Statistics:");
var monthlyStats = monthlySales.GetStatistics();
foreach (var stat in monthlyStats)
{
    Console.WriteLine($"{stat.Key}: {stat.Value:C}");
}

// Outlier analysis
Console.WriteLine("\nOutlier Analysis:");
var potentialOutliers = new[] { 3500.00m, 750.25m, 2250.75m };
foreach (var amount in potentialOutliers)
{
    if (monthlySales.IsOutlier(amount))
    {
        Console.WriteLine($"{amount:C} is identified as an outlier!");
    }
    else
    {
        Console.WriteLine($"{amount:C} is within normal range");
    }
}