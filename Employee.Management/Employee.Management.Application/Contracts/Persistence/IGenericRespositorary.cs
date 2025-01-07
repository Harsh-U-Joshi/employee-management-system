namespace Employee.Management.Application.Contracts.Persistence
{
    public interface IGenericRespositorary<T> where T : class
    {
        Task<T?> GetAsync(string id);

        Task<T> AddAsync(string userId, T entity);

        Task UpdateAsync(string userId, T entity);

        Task DeleteAsync(string userId, string id);

        Task<bool> Exists(string id);
    }
}
