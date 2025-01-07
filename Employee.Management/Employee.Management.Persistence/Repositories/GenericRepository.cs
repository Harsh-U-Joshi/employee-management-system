using Employee.Management.Application.Contracts.Persistence;
using Employee.Management.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Employee.Management.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRespositorary<T> where T : BaseDomainEntity
    {
        private readonly EmployeeManagementDbContext _dbContext;

        public GenericRepository(EmployeeManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> AddAsync(string userId, T entity)
        {
            entity.CreatedBy = userId;

            entity.LastModifiedBy = userId;

            await _dbContext.AddAsync(entity);

            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(string userId, T entity)
        {
            entity.LastModifiedBy = userId;

            _dbContext.Entry(entity).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string userId, string id)
        {
            var entity = await GetAsync(id);

            if (entity is not null)
            {
                entity.IsDeleted = true;

                entity.LastModifiedBy = userId;

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<T?> GetAsync(string id)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<bool> Exists(string id)
        {
            var entity = await GetAsync(id);

            return entity is not null;
        }
    }
}
