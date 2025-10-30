using Shared;
using Shared.Models;

namespace QueryableExtensions;

public class ProductRepository(IQueryable<Product> products)
{
    private readonly IQueryable<Product> _products = products; // From DbContext

    public PagedResult<Product> GetProducts(
        string? category = null,
        decimal? minPrice = null,
        bool? inStock = null,
        int pageNumber = 1,
        int pageSize = 10,
        string sortBy = "Name")
    {
        var query = _products
            .WhereIf(!string.IsNullOrEmpty(category), p => p.Category == category)
            .WhereIf(minPrice.HasValue, p => p.Price >= (minPrice.HasValue ? minPrice.Value : 0))
            .WhereIf(inStock.HasValue, p => p.InStock == (inStock.HasValue ? inStock.Value : false))
            .OrderByProperty(sortBy);

        return query.ToPagedQuery(pageNumber, pageSize);
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _products
            .WhereIf(id > 0, p => p.Id == id)
            .FirstOrDefaultAsync();
    }
}
