
namespace BloggingPlatform.Domain.Entities.Base
{
    /// <summary>
    /// This defines an abstract class to be used for every other entity on the database
    /// Defining created, updated and deleted properties help making the application scalable
    /// </summary>
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; }
        public DateTime Created { get; protected set; }
        public string? CreatedBy { get; protected set; }
        public DateTime? Updated { get; protected set; }
        public string? UpdatedBy { get; protected set; }
        public DateTime? Deleted { get; protected set; }
        public string? DeletedBy { get; protected set; }
        public bool IsDeleted => Deleted != null;
        public void SetCreatedBy(string createdBy) => CreatedBy = createdBy;
    }
}
