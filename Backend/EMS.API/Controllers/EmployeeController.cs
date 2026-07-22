using EMS.API.DTOs.Employee;
using EMS.API.Interfaces.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        #region Create Employee

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateEmployee(
            CreateEmployeeRequestDto request)
        {
            var response = await _employeeService.CreateEmployeeAsync(request);

            return Ok(response);
        }

        #endregion

        #region Get All Employees

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var response = await _employeeService.GetEmployeesAsync();

            return Ok(response);
        }

        #endregion

        #region Get Employee By Id

        [HttpGet("{employeeId:int}")]
        public async Task<IActionResult> GetEmployeeById(int employeeId)
        {
            var response = await _employeeService.GetEmployeeByIdAsync(employeeId);

            return Ok(response);
        }

        #endregion

        #region Update Employee

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEmployee(
            UpdateEmployeeRequestDto request)
        {
            var response = await _employeeService.UpdateEmployeeAsync(request);

            return Ok(response);
        }

        #endregion

        #region Delete Employee

        [HttpDelete("{employeeId:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            var response = await _employeeService.DeleteEmployeeAsync(employeeId);

            return Ok(response);
        }

        #endregion

        #region Search Employees

        [HttpGet("search")]
        public async Task<IActionResult> SearchEmployees(
            [FromQuery] EmployeeSearchRequestDto request)
        {
            var response = await _employeeService.SearchEmployeesAsync(request);

            return Ok(response);
        }

        #endregion
    }
}