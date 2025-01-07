using AutoMapper;
using Employee.Management.MVC.Contracts;
using Employee.Management.MVC.Models.Common;
using Employee.Management.MVC.Models.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

namespace Employee.Management.MVC.Controllers
{
    public class EmployeeController : Controller
    {
        public IEmployeeService _employeeService { get; }
        public IDepartmentService _departmentService { get; }
        public IPositionService _positionService { get; }
        public IMapper _mapper { get; }

        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService, IPositionService positionService, IMapper mapper)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
            _positionService = positionService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string search, string sortField, string sortOrder, CancellationToken cancellationToken, int page = 1, int pageSize = 10)
        {
            var queryModel = new QueryParamModel()
            {
                Search = search,
                PageIndex = page,
                PageSize = pageSize,
                SortColumn = sortField,
                OrderBy = sortOrder
            };

            var jsonResponse = await _employeeService.GetAllEmployee(queryModel, cancellationToken);

            if (!jsonResponse.Success)
                return ViewBag.ErrorMessage = jsonResponse.Message;

            ViewBag.SortOrder = search ?? string.Empty;
            ViewBag.SortField = sortField ?? string.Empty;
            ViewBag.Search = sortOrder ?? string.Empty;

            ViewBag.PageSize = pageSize;
            ViewBag.PageIndex = page;
            ViewBag.RecordsTotalCount = jsonResponse.Data.RecordsTotalCount;
            ViewBag.RecordsFilteredCount = jsonResponse.Data.RecordsFilteredCount;

            return View(jsonResponse.Data.Data);
        }

        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
            var departments = await _departmentService.GetAllDepartment(cancellationToken);

            var positions = await _positionService.GetAllPosition(cancellationToken);

            if (departments.Success)
                ViewBag.Departments = new SelectList(departments.Data, "Id", "Name");

            if (positions.Success)
                ViewBag.Positions = new SelectList(positions.Data, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployee createEmployeeRequest, CancellationToken cancellationToken)
        {
            var jsonResponse = await _employeeService.CreateEmployee(createEmployeeRequest, cancellationToken);

            if (!jsonResponse.Success)
                ViewBag.ErrorMessage = jsonResponse.Message;
            else
                return RedirectToAction("Index", "Employee");

            return View();
        }

        public async Task<IActionResult> Edit(string employeeId, CancellationToken cancellationToken)
        {
            var jsonResponse = await _employeeService.GetEmployeeById(employeeId, cancellationToken);

            var departments = await _departmentService.GetAllDepartment(cancellationToken);

            var positions = await _positionService.GetAllPosition(cancellationToken);

            if (departments.Success)
                ViewBag.Departments = new SelectList(departments.Data, "Id", "Name");

            if (positions.Success)
                ViewBag.Positions = new SelectList(positions.Data, "Id", "Name");

            if (!jsonResponse.Success)
            {
                ViewBag.ErrorMessage = jsonResponse.Message;
                return View(new UpdateEmployee());
            }

            var updateViewModel = _mapper.Map<UpdateEmployeeViewModel>(jsonResponse.Data);

            return View(updateViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmployee(UpdateEmployeeViewModel updateEmployee, CancellationToken cancellationToken)
        {
            var requestPayload = _mapper.Map<UpdateEmployee>(updateEmployee);

            var jsonResponse = await _employeeService.UpdateEmployee(requestPayload, cancellationToken);

            if (!jsonResponse.Success)
                ViewBag.ErrorMessage = jsonResponse.Message;
            else
                return RedirectToAction("Index", "Employee");

            return View();
        }

        public async Task<IActionResult> Delete(string employeeId, CancellationToken cancellationToken)
        {
            var jsonResponse = await _employeeService.DeleteEmployee(employeeId, cancellationToken);

            if (!jsonResponse.Success)
                ViewBag.ErrorMessage = jsonResponse.Message;
            else
                return RedirectToAction("Index", "Employee");

            return View();
        }
    }
}
