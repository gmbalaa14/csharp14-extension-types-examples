# C# 14 Extension Types - Real-World Examples

![C# Version](https://img.shields.io/badge/C%23-14-blue)
![.NET Version](https://img.shields.io/badge/.NET-9.0-purple)
![License](https://img.shields.io/badge/license-MIT-green)
![Stars](https://img.shields.io/github/stars/gmbalaa14/csharp14-extension-types-examples)

> Comprehensive examples of C# 14's Extension Types feature with real-world use cases

## 🎯 About This Repository

This repository demonstrates the power of **Extension Types** in C# 14 through 6 production-ready generic extension implementations.

After 15+ years of working with extension methods, wrapper classes, and adapter patterns, extension types finally provide a first-class solution for extending types you don't control.

---

## 🚀 What Are Extension Types?

Extension types allow you to extend existing types with:
- ✅ **Properties** (not just methods!)
- ✅ **Indexers**
- ✅ **Operators**
- ✅ **Generic constraints**

All without modifying the original type or creating wrapper classes.

### Before (Extension Methods - C# 3.0)
```csharp
public static class OrderExtensions
{
    public static decimal GetAverage(this Order order)
    {
        return order.Items.Count > 0
            ? order.TotalAmount / order.Items.Count
            : 0;
    }
}

// Usage (method call)
order.GetAverage()
```

### After (Extension Types - C# 14)
```csharp
public static partial class OrderExtensions
{
    // Properties and categorization logic for analytics
    extension(Order order)
    {
        public decimal AverageItemValue =>
            order.Items.Count > 0
                ? order.TotalAmount / order.Items.Count
                : 0;
    }
}

// Usage (property access!)
order.AverageItemValue
```
---

## 📦 What's Inside

### 1. Collection Statistics (`List<T>`)
Extend all `List<T>` types with statistical operations:
- Median, Average, Min, Max
- Outlier detection
- Statistical summaries

**[View Code →](src/1-CollectionExtensions/)**

### 2. Business Enumerables (`IEnumerable<T>`)
Add pagination, batching, and safe operations:
- Paged results with metadata
- Batch processing
- Safe First/Single operations

**[View Code →](src/2-EnumerableExtensions/)**

### 3. Safe Dictionary Operations (`Dictionary<TKey, TValue>`)
Safe access patterns and transformations:
- GetValueOrDefault with custom indexer
- Map keys/values
- Merge with conflict resolution

**[View Code →](src/3-DictionaryExtensions/)**

### 4. Result Pattern (`Result<T>`)
Railway-oriented programming with monadic operations:
- Map, Bind, Match operations
- Success/Failure pipelines
- Custom operators

**[View Code →](src/4-ResultPattern/)**

### 5. Repository Pattern (`IQueryable<T>`)
LINQ extensions for repositories:
- Conditional filtering (WhereIf)
- Dynamic sorting
- Pagination with metadata

**[View Code →](src/5-QueryableExtensions/)**

### 6. Entity Framework Auditing (`IEntity`)
Automatic audit trail management:
- Created/Updated tracking
- Soft delete support
- Validation methods

**[View Code →](src/6-EntityExtensions/)**

---

## 🛠️ Requirements

- **.NET 9.0 SDK** (or later)
- **C# 14** language version
- **Visual Studio 2024** or **JetBrains Rider 2024.3+** (for best experience)

---

## 🚦 Getting Started

### 1. Clone the repository
```bash
git clone https://github.com/YOUR-USERNAME/csharp14-extension-types-examples.git
cd csharp14-extension-types-examples
```

### 2. Open the solution
```bash
cd src
dotnet restore
dotnet build
```

### 3. Run examples
```bash
cd 1-CollectionExtensions
dotnet run
```

---

## 📚 Documentation

- **[Migration Guide](docs/migration-guide.md)** - Moving from extension methods to extension types
- **[Best Practices](docs/best-practices.md)** - Naming conventions and patterns
- **[Comparison Chart](docs/comparison-chart.md)** - Extension types vs other patterns
- **[LinkedIn Carousel](docs/linkedin-carousel.pdf)** - Visual explanation of concepts

---

## 🎓 Usage Examples

Each folder contains:
- ✅ Complete extension type implementation
- ✅ Real-world usage scenarios
- ✅ Before/After comparisons
- ✅ Documentation and comments

### Quick Example: List Statistics
```csharp
var salesAmounts = new List { 100m, 250m, 175m, 300m };

// Use extension properties
Console.WriteLine($"Median: {salesAmounts.Median}");
Console.WriteLine($"Average: {salesAmounts.Average}");
Console.WriteLine($"Max: {salesAmounts.Max}");

// Use extension methods
var stats = salesAmounts.GetStatistics();
bool isOutlier = salesAmounts.IsOutlier(500m);
```

---

## 🤝 Contributing

Contributions are welcome! Here's how you can help:

1. **Add more examples** - Share your real-world use cases
2. **Improve documentation** - Better explanations, more examples
3. **Write tests** - Help increase test coverage
4. **Report issues** - Found a bug or have a suggestion?

### Contribution Ideas:
- [ ] Add benchmarks comparing extension methods vs extension types
- [ ] Create examples for ASP.NET Core integration
- [ ] Add examples for Blazor components
- [ ] Write examples for database providers (Dapper, EF Core)
- [ ] Create video tutorials

**[Read Contribution Guidelines →](CONTRIBUTING.md)**

---

## 📊 Comparison: Extension Types vs Alternatives

| Pattern | Modify Original? | Properties? | Operators? | Type Safety | Boilerplate |
|---------|-----------------|-------------|------------|-------------|-------------|
| **Extension Types** | ❌ No | ✅ Yes | ✅ Yes | ✅ Full | 🟢 Low |
| Extension Methods | ❌ No | ❌ No | ❌ No | ✅ Full | 🟡 Medium |
| Wrapper Classes | ❌ No | ✅ Yes | ✅ Yes | ✅ Full | 🔴 High |
| Adapter Pattern | ❌ No | ✅ Yes | ✅ Yes | ✅ Full | 🔴 High |
| Partial Classes | ⚠️ Same assembly | ✅ Yes | ✅ Yes | ✅ Full | 🟡 Medium |
| Inheritance | ⚠️ Requires control | ✅ Yes | ✅ Yes | ✅ Full | 🟡 Medium |

---

## 🎯 Real-World Use Cases

- **Vendor Library Extensions** - Add missing functionality to third-party libraries
- **Domain-Driven Design** - Model different bounded contexts' views of entities
- **Clean Architecture** - Separate concerns without polluting domain models
- **API Client Wrappers** - Enhance API response types
- **CQRS Pattern** - Add query-specific properties to command models
- **Specification Pattern** - Build composable business rules

---

## 📝 License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

---

## 🌟 Show Your Support

If you find this repository helpful:

⭐ **Star this repo** - Helps others discover it  
🍴 **Fork it** - Experiment with your own extensions  
📢 **Share it** - Spread the word on social media  
💬 **Discuss it** - Open issues with questions or ideas  

---

## 👤 Author

**[Your Name]**

- LinkedIn: [Your LinkedIn Profile](YOUR_LINKEDIN_URL)
- Twitter: [@YourHandle](YOUR_TWITTER)
- Blog: [Your Blog](YOUR_BLOG_URL)
- GitHub: [@YourUsername](https://github.com/YOUR-USERNAME)

---

## 🙏 Acknowledgments

- Microsoft C# Design Team for creating extension types
- The .NET community for feedback and discussions
- All contributors to this repository

---

## 📅 Changelog

### v1.0.0 (2025-01-XX)
- ✨ Initial release with 6 generic extension examples
- 📚 Complete documentation
- 🧪 Basic test coverage
- 📺 LinkedIn carousel content

**[View Full Changelog →](CHANGELOG.md)**

---

## 🔗 Related Resources

- [C# 14 Language Specification](https://learn.microsoft.com/en-us/dotnet/csharp/)
- [Extension Types Proposal](https://github.com/dotnet/csharplang)
- [.NET Blog - Extension Types](https://devblogs.microsoft.com/dotnet/)
- [My LinkedIn Article Series on C# 14](YOUR_LINKEDIN_URL)

---

<div align="center">

**Made with ❤️ for the .NET Community**

[Report Bug](https://github.com/YOUR-USERNAME/csharp14-extension-types-examples/issues) · 
[Request Feature](https://github.com/YOUR-USERNAME/csharp14-extension-types-examples/issues) · 
[Discussions](https://github.com/YOUR-USERNAME/csharp14-extension-types-examples/discussions)

</div>
```

---
