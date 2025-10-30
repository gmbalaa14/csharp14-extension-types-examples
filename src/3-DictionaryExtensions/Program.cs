// See https://aka.ms/new-console-template for more information
using DictionaryExtensions;

Console.WriteLine("Testing Dictionary Extensions:\n");

var configManager = new ConfigurationManager();
Console.WriteLine("Managing Settings:");
Console.WriteLine("-----------------");
configManager.ManageSettings();

// Additional examples to demonstrate dictionary extensions
var settings = new Dictionary<string, string>
{
    ["DatabaseConnection"] = "Server=localhost;Database=TestDB",
    ["CacheTimeout"] = "300",
    ["LogLevel"] = "INFO",
    ["MaxConnections"] = "100"
};

Console.WriteLine("\nCustom Settings Management:");
Console.WriteLine("-------------------------");

// Demonstrate safe access
Console.WriteLine($"Database Connection: {settings.GetValueOrDefault("DatabaseConnection", "No connection string")}");
Console.WriteLine($"Unknown Setting: {settings.GetValueOrDefault("NonExistent", "Default Value")}");

// Demonstrate value transformation
var upperSettings = settings.MapValues(v => v.ToUpper());
Console.WriteLine("\nUppercase Settings:");
foreach (var setting in upperSettings)
{
    Console.WriteLine($"{setting.Key}: {setting.Value}");
}

// Demonstrate value filtering
var numericSettings = settings.WhereValue(v => int.TryParse(v, out _));
Console.WriteLine("\nNumeric Settings:");
foreach (var setting in numericSettings)
{
    Console.WriteLine($"{setting.Key}: {setting.Value}");
}

// Demonstrate dictionary merging
var overrideSettings = new Dictionary<string, string>
{
    ["LogLevel"] = "DEBUG",
    ["NewSetting"] = "TestValue",
    ["MaxConnections"] = "200"
};

var mergedSettings = settings.MergeWith(overrideSettings, (existing, newValue) => newValue);
Console.WriteLine("\nMerged Settings:");
foreach (var setting in mergedSettings)
{
    Console.WriteLine($"{setting.Key}: {setting.Value}");
}

// Check dictionary properties
Console.WriteLine($"\nSettings Status:");
Console.WriteLine($"Is Empty: {settings.IsEmpty}");
Console.WriteLine($"Setting Count: {settings.Count}");