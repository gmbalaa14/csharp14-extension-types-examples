namespace DictionaryExtensions;

public class ConfigurationManager
{
    public void ManageSettings()
    {
        var settings = new Dictionary<string, string>
        {
            ["ApiUrl"] = "https://api.example.com",
            ["Timeout"] = "30",
            ["MaxRetries"] = "3"
        };

        // Safe access with default
        var apiUrl = settings.GetValueOrDefault("ApiUrl", "https://default.com");
        var unknownSetting = settings.GetValueOrDefault("Unknown", "N/A");

        // Using indexer with default
        // TODO: Enable this when C# supports indexers with default parameters
        // var debug = settings["DebugMode", "false"];
        var debug = settings.GetValueOrDefault("DebugMode", "false");

        // Transform values
        var uppercaseSettings = settings.MapValues(v => v.ToUpper());

        // Filter by value
        var numericSettings = settings.WhereValue(v => int.TryParse(v, out _));

        // Merge with another dictionary
        var overrides = new Dictionary<string, string>
        {
            ["Timeout"] = "60",
            ["NewSetting"] = "value"
        };

        var merged = settings.MergeWith(overrides, (existing, newValue) => newValue);

        Console.WriteLine($"Is Empty: {settings.IsEmpty}");
        Console.WriteLine($"Setting Count: {settings.Count}");
    }
}
