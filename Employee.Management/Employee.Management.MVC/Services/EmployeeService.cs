using Employee.Management.MVC.Contracts;
using Employee.Management.MVC.Models.Common;
using Employee.Management.MVC.Models.Employee;
using Employee.Management.MVC.Utitlies;

namespace Employee.Management.MVC.Services
{
    public class EmployeeService : IEmployeeService
    {
        public IBaseHttpService _baseHttpService { get; }

        public EmployeeService(IBaseHttpService baseHttpService)
        {
            _baseHttpService = baseHttpService;

            _baseHttpService.includeToken = true;
        }

        public async Task<GenericResponse<GridDataResponse<List<EmployeeBasicDetails>>>> GetAllEmployee(QueryParamModel queryParamModel, CancellationToken cancellationToken)
        {
            return await _baseHttpService.PostAsync<QueryParamModel, GridDataResponse<List<EmployeeBasicDetails>>>(ApiEndpoints.EmployeeGridApi, queryParamModel, cancellationToken);
        }

        public async Task<GenericResponse<EmployeeDetails>> GetEmployeeById(string employeeId, CancellationToken cancellationToken)
        {
            return await _baseHttpService.GetAsync<EmployeeDetails>($"{ApiEndpoints.GetEmployeeByIdApi}/{employeeId}", cancellationToken);
        }

        public async Task<GenericResponse<object>> CreateEmployee(CreateEmployee createEmployee, CancellationToken cancellationToken)
        {
            return await _baseHttpService.PostAsync(ApiEndpoints.CreateEmployeeApi, createEmployee, cancellationToken);
        }

        public async Task<GenericResponse<object>> UpdateEmployee(UpdateEmployee updateEmployee, CancellationToken cancellationToken)
        {
            return await _baseHttpService.PutAsync(ApiEndpoints.UpdateEmployeeApi, updateEmployee, cancellationToken);
        }

        public async Task<GenericResponse<object>> DeleteEmployee(string employeeId, CancellationToken cancellationToken)
        {
            return await _baseHttpService.DeleteAsync($"{ApiEndpoints.UpdateEmployeeApi}/{employeeId}", cancellationToken);
        }
    }

    public class DepartmentService : IDepartmentService
    {
        public IBaseHttpService _baseHttpService { get; }

        public DepartmentService(IBaseHttpService baseHttpService)
        {
            _baseHttpService = baseHttpService;

            _baseHttpService.includeToken = true;
        }

        public async Task<GenericResponse<List<CommonDropDownResponse>>> GetAllDepartment(CancellationToken cancellationToken)
        {
            return await _baseHttpService.GetAsync<List<CommonDropDownResponse>>(ApiEndpoints.DepartmentDropDownApi, cancellationToken);
        }
    }

    public class PositionService : IPositionService
    {
        public IBaseHttpService _baseHttpService { get; }

        public PositionService(IBaseHttpService baseHttpService)
        {
            _baseHttpService = baseHttpService;

            _baseHttpService.includeToken = true;
        }

        public async Task<GenericResponse<List<CommonDropDownResponse>>> GetAllPosition(CancellationToken cancellationToken)
        {
            return await _baseHttpService.GetAsync<List<CommonDropDownResponse>>(ApiEndpoints.PositionDropDownApi, cancellationToken);
        }
    }
}
