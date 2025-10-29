namespace CollectionExtensions;

public class AnalyticsDemo
{
    public void AnalyzeSalesData()
    {
        var salesAmounts = new List<decimal>
        {
            100m, 250m, 175m, 300m, 125m, 500m, 200m
        };

        // Use extension properties
        Console.WriteLine($"Median Sale: {salesAmounts.Median}");
        Console.WriteLine($"Average Sale: {salesAmounts.Average}");
        Console.WriteLine($"Max Sale: {salesAmounts.Max}");
        Console.WriteLine($"Min Sale: {salesAmounts.Min}");

        // Use extension method
        var stats = salesAmounts.GetStatistics();
        foreach (var stat in stats)
        {
            Console.WriteLine($"{stat.Key}: {stat.Value}");
        }

        // Check for outliers
        if (salesAmounts.IsOutlier(500m))
        {
            Console.WriteLine("500 is an outlier!");
        }
    }
}

