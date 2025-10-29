namespace Shared;

// Soft delete interface
public interface ISoftDeletable : IEntity
{
    bool IsDeleted { get; set; }
    DateTime? DeletedAt { get; set; }
    string? DeletedBy { get; set; }
}
