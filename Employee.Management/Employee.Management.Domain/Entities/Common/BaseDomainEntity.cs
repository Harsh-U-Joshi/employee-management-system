namespace Employee.Management.Domain.Entities.Common;

public class BaseDomainEntity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public DateTime CreatedDateTimeUTC { get; set; }

    public string CreatedBy { get; set; }

    public DateTime LastModifiedDateTimeUTC { get; set; }

    public string LastModifiedBy { get; set; }

    public bool IsDeleted { get; set; }
}
