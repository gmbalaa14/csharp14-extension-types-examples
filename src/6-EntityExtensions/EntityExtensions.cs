using Shared;

namespace EntityExtensions;

public static class EntityExtensions
{
    extension<T>(T entity)
        where T : class, IEntity
    {
        // Audit properties
        public bool IsNew => entity.Id == 0;

        public bool IsModified => entity.UpdatedAt.HasValue;

        public TimeSpan Age => DateTime.UtcNow - entity.CreatedAt;

        public int DaysSinceCreated => (int)entity.Age.TotalDays;

        // Audit methods
        public T MarkAsCreated(string userId)
        {
            entity.CreatedAt = DateTime.UtcNow;

            if (entity is IAuditable auditable)
            {
                auditable.CreatedBy = userId;
            }

            return entity;
        }

        public T MarkAsUpdated(string userId)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            if (entity is IAuditable auditable)
            {
                auditable.UpdatedBy = userId;
            }

            return entity;
        }

        public T MarkAsDeleted(string userId)
        {
            var softDeletableEntity = entity as ISoftDeletable;
            softDeletableEntity?.IsDeleted = true;
            softDeletableEntity?.DeletedAt = DateTime.UtcNow;
            softDeletableEntity?.DeletedBy = userId;

            return entity;
        }

        // Validation
        public bool Validate(out List<string> errors)
        {
            errors = [];

            if (entity.Id < 0)
            {
                errors.Add("Id cannot be negative");
            }

            if (entity.CreatedAt > DateTime.UtcNow)
            {
                errors.Add("CreatedAt cannot be in the future");
            }

            if (entity is IAuditable auditable)
            {
                if (string.IsNullOrWhiteSpace(auditable.CreatedBy))
                {
                    errors.Add("CreatedBy is required");
                }
            }

            return errors.Count == 0;
        }
    }
}