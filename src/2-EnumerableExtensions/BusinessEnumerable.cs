using Shared;

namespace EnumerableExtensions;

public static class BusinessEnumerable
{
    extension<T>(IEnumerable<T> source)
        where T : class
    {
        // Properties
        public int TotalCount => source.Count();

        public bool IsEmpty => !source.Any();

        public bool HasMultiple => source.Skip(1).Any();

        // Pagination
        public IEnumerable<T> Page(int pageNumber, int pageSize)
            => source.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        public PagedResult<T> ToPagedResult(int pageNumber, int pageSize)
        {
            var items = source.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            var totalCount = source.Count();

            return new PagedResult<T>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }

        // Batch processing
        public IEnumerable<IReadOnlyList<T>> Batch(int batchSize)
        {
            if (batchSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(batchSize), "Batch size must be greater than zero.");

            var batch = new List<T>(batchSize);

            foreach (var item in source)
            {
                batch.Add(item);
                if (batch.Count == batchSize)
                {
                    yield return batch.AsReadOnly();
                    batch = new List<T>(batchSize);
                }
            }

            if (batch.Count > 0)
                yield return batch.AsReadOnly();
        }

        // Safe retrieval (reference or value types)
        public T? FirstOrNull()
            => source.FirstOrDefault();

        public T? SingleOrNull()
        {
            using var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext()) return null;
            var first = enumerator.Current;
            return enumerator.MoveNext() ? null : first;
        }
    }
}
