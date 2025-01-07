using Employee.Management.Domain.Entities.Common;

namespace Employee.Management.Application.Contracts.Persistence
{
    public interface IErrorLogger
    {
        Task<bool> SaveErrorLogs(ErrorLog errorLog);
    }
}
