using Employee.Management.Domain.Entities.Common;

namespace Employee.Management.Domain.Entities;

public class Position : BaseDomainEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
}
