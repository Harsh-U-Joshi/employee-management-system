using Employee.Management.MVC.Models.Common;
using Employee.Management.MVC.Models.Employee;

namespace Employee.Management.MVC.Contracts
{
    public interface IEmployeeService
    {
        Task<GenericResponse<GridDataResponse<List<EmployeeBasicDetails>>>> GetAllEmployee(QueryParamModel queryParamModel, CancellationToken cancellationToken);

        Task<GenericResponse<EmployeeDetails>> GetEmployeeById(string employeeId, CancellationToken cancellationToken);

        Task<GenericResponse<object>> CreateEmployee(CreateEmployee createEmployee, CancellationToken cancellationToken);

        Task<GenericResponse<object>> UpdateEmployee(UpdateEmployee updateEmployee, CancellationToken cancellationToken);

        Task<GenericResponse<object>> DeleteEmployee(string employeeId, CancellationToken cancellationToken);
    }

    public interface IDepartmentService
    {
        Task<GenericResponse<List<CommonDropDownResponse>>> GetAllDepartment(CancellationToken cancellationToken);
    }

    public interface IPositionService
    {
        Task<GenericResponse<List<CommonDropDownResponse>>> GetAllPosition(CancellationToken cancellationToken);
    }
}
