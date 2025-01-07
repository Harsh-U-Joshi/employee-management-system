using Employee.Management.Domain.Entities.Common;

namespace Employee.Management.Domain.Entities;

public class Department : BaseDomainEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; }
}
