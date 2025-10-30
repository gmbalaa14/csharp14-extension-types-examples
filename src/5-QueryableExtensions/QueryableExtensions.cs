using Shared;
using System.Linq.Expressions;

namespace QueryableExtensions;

public static class QueryableExtensions
{
    extension<T>(IQueryable<T> query)
    where T : class
    {
        // Specification pattern properties
        public int TotalCount => query.Count();

        public bool HasAny => query.Any();

        // Conditional filtering
        public IQueryable<T> WhereIf(bool condition, Expression<Func<T, bool>> predicate)
            => condition ? query.Where(predicate) : query;

        // Pagination with metadata
        public PagedResult<T> ToPagedQuery(int pageNumber, int pageSize)
        {
            var totalCount = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .ToList();

            return new PagedResult<T>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }

        // Dynamic sorting
        public IQueryable<T> OrderByProperty(string propertyName, bool descending = false)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda(property, parameter);

            var methodName = descending ? "OrderByDescending" : "OrderBy";
            var method = typeof(Queryable)
                .GetMethods()
                .First(m => m.Name == methodName && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), property.Type);

            return (IQueryable<T>)method.Invoke(null, new object[] { query, lambda })!;
        }

        // Include related data (for EF Core)
        public IQueryable<T> IncludeIf(bool condition, Expression<Func<T, object>> navigationProperty)
        {
            // Normally: return condition ? query.Include(navigationProperty) : query;
            // (kept simple here to avoid EF dependency)
            return query;
        }

        // Async operations (simulated)
        public async Task<List<T>> ToListAsync(CancellationToken cancellationToken = default)
            => await Task.Run(() => query.ToList(), cancellationToken);

        public async Task<T?> FirstOrDefaultAsync(CancellationToken cancellationToken = default)
            => await Task.Run(() => query.FirstOrDefault(), cancellationToken);
    }
}