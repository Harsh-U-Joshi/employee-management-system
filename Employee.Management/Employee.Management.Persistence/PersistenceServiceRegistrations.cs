using Employee.Management.Application.Contracts.Persistence;
using Employee.Management.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Employee.Management.Persistence
{
    public static class PersistenceServiceRegistrations
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EmployeeManagementDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("EmployeeManagementConnectionString"));
            });

            services.AddScoped(typeof(IGenericRespositorary<>), typeof(GenericRepository<>));

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            services.AddScoped<IPositionRepository, PositionRepository>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IErrorLogger, ErrorLogger>();

            return services;
        }
    }
}
