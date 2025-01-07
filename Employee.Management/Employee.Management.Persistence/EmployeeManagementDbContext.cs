using Employee.Management.Domain.Entities;
using Employee.Management.Domain.Entities.ApplicationUser;
using Employee.Management.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Employee.Management.Persistence;

public class EmployeeManagementDbContext : DbContext
{
    public EmployeeManagementDbContext(DbContextOptions<EmployeeManagementDbContext> options) : base(options)
    {

    }

    public DbSet<EmployeeDetail> EmployeeDetail { get; set; }
    public DbSet<Department> Department { get; set; }
    public DbSet<Position> Position { get; set; }
    public DbSet<ApplicationUser> ApplicationUser { get; set; }
    public DbSet<ErrorLog> ErrorLog { get; set; }

    public virtual async Task<int> SaveChangesAsync()
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseDomainEntity>()
            .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
        {
            entry.Entity.LastModifiedDateTimeUTC = DateTime.UtcNow;

            if (entry.State == EntityState.Added)
                entry.Entity.CreatedDateTimeUTC = DateTime.UtcNow;

        }

        var result = await base.SaveChangesAsync();

        return result;
    }
}
