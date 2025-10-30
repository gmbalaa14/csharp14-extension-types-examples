namespace CollectionExtensions;

// Extend ALL List<T> types with statistical methods
public static class ListStatistics
{
    extension<T>(List<T> source)
    where T : struct, IComparable<T>, IConvertible
    {
        // Median
        public T? Median
        {
            get
            {
                if (source.Count == 0)
                    return null;

                var sorted = source.OrderBy(x => x).ToList();
                int mid = sorted.Count / 2;

                // For simplicity, return middle value
                // (could interpolate for true median if even count)
                return sorted[mid];
            }
        }

        // Average
        public double Average
        {
            get
            {
                if (source.Count == 0)
                    return 0;

                return source.Sum(x => Convert.ToDouble(x)) / source.Count;
            }
        }

        // Max and Min
        public T? Max => source.Count > 0 ? source.Max() : null;

        public T? Min => source.Count > 0 ? source.Min() : null;

        // Statistics Summary
        public Dictionary<string, object> GetStatistics()
        {
            var avg = source.Average;
            var max = source.Max;
            var min = source.Min;
            var median = source.Median;

            return new Dictionary<string, object>
            {
                ["Count"] = source.Count,
                ["Average"] = avg,
                ["Median"] = median ?? default(T),
                ["Max"] = max ?? default(T),
                ["Min"] = min ?? default(T),
                ["Range"] = (max.HasValue && min.HasValue)
                    ? Convert.ToDouble(max.Value) - Convert.ToDouble(min.Value)
                    : 0d
            };
        }

        // Outlier Detection (Z-Score Method)
        public bool IsOutlier(T value, double threshold = 2.0)
        {
            if (source.Count < 2)
                return false;

            var avg = source.Average;
            var stdDev = Math.Sqrt(
                source.Sum(x => Math.Pow(Convert.ToDouble(x) - avg, 2)) / source.Count
            );

            if (stdDev == 0)
                return false;

            var zScore = Math.Abs((Convert.ToDouble(value) - avg) / stdDev);
            return zScore > threshold;
        }
    }
}
