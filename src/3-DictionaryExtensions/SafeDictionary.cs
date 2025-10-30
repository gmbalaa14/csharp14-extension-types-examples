namespace DictionaryExtensions;

public static class SafeDictionary
{
    extension<TKey, TValue>(Dictionary<TKey, TValue> source)
        where TKey : notnull
    {
        // Safe access with default
        public TValue GetValueOrDefault(TKey key, TValue defaultValue = default!)
            => source.TryGetValue(key, out var value) ? value : defaultValue;

        // Indexer with default fallback
        // TODO: Enable this when C# supports indexers with default parameters
        //public TValue this[TKey key, TValue defaultValue]
        //    => GetValueOrDefault(key, defaultValue);

        // Transformation methods
        public Dictionary<TKey, TResult> MapValues<TResult>(Func<TValue, TResult> mapper)
            => source.ToDictionary(kvp => kvp.Key, kvp => mapper(kvp.Value));

        public Dictionary<TResult, TValue> MapKeys<TResult>(Func<TKey, TResult> mapper)
            where TResult : notnull
            => source.ToDictionary(kvp => mapper(kvp.Key), kvp => kvp.Value);

        // Filtering
        public Dictionary<TKey, TValue> WhereValue(Func<TValue, bool> predicate)
            => source.Where(kvp => predicate(kvp.Value))
                     .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        public Dictionary<TKey, TValue> WhereKey(Func<TKey, bool> predicate)
            => source.Where(kvp => predicate(kvp.Key))
                     .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

        // Merge operations
        public Dictionary<TKey, TValue> MergeWith(
            Dictionary<TKey, TValue> other,
            Func<TValue, TValue, TValue>? conflictResolver = null)
        {
            var result = new Dictionary<TKey, TValue>(source);

            foreach (var kvp in other)
            {
                if (result.TryGetValue(kvp.Key, out var existingValue) && conflictResolver is not null)
                    result[kvp.Key] = conflictResolver(existingValue, kvp.Value);
                else
                    result[kvp.Key] = kvp.Value;
            }

            return result;
        }

        // Properties
        public bool IsEmpty => source.Count == 0;

        public IEnumerable<TKey> AllKeys => source.Keys;

        public IEnumerable<TValue> AllValues => source.Values;
    }
}
