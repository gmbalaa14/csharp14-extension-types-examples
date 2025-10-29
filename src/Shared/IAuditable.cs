namespace Shared;

// Auditable entity interface
public interface IAuditable : IEntity
{
    string CreatedBy { get; set; }
    string? UpdatedBy { get; set; }
}
