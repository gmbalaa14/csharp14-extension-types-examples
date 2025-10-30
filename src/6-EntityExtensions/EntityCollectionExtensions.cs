using Shared;

namespace EntityExtensions;

public static class EntityCollectionExtensions
{
    extension<T>(IEnumerable<T> entities)
        where T : class, IEntity
    {
        // Filter active (non-deleted) items
        public IEnumerable<T> Active()
            => entities.Where(e => !(e as ISoftDeletable)!.IsDeleted);

        // Get recently created items
        public IEnumerable<T> CreatedInLast(int days)
        {
            var cutoff = DateTime.UtcNow.AddDays(-days);
            return entities.Where(e => e.CreatedAt >= cutoff);
        }

        // Get modified items
        public IEnumerable<T> Modified()
            => entities.Where(e => e.UpdatedAt.HasValue);

        // Bulk operations
        public void MarkAllAsUpdated(string userId)
        {
            foreach (var entity in entities)
            {
                // Reuse single-entity extension type
                entity.MarkAsUpdated(userId);
            }
        }

        public void SoftDeleteAll(string userId)
        {
            foreach (var entity in entities)
            {
                entity.MarkAsDeleted(userId);
            }
        }

        // Statistics
        public Dictionary<string, object> GetStatistics()
        {
            var list = entities.ToList();

            return new Dictionary<string, object>
            {
                ["TotalCount"] = list.Count,
                ["NewCount"] = list.Count(e => e.IsNew),
                ["ModifiedCount"] = list.Count(e => e.IsModified),
                ["AverageAgeDays"] = list.Average(e => e.DaysSinceCreated),
                ["OldestDate"] = list.Min(e => e.CreatedAt),
                ["NewestDate"] = list.Max(e => e.CreatedAt)
            };
        }
    }
}
