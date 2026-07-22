using EMS.API.DTOs.Department;
using EMS.API.Interfaces.Department;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateDepartment(
            CreateDepartmentRequestDto request)
        {
            var result = await _departmentService.CreateDepartmentAsync(request);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var result = await _departmentService.GetDepartmentsAsync();

            return Ok(result);
        }

        [HttpGet("{departmentId}")]
        public async Task<IActionResult> GetDepartmentById(int departmentId)
        {
            var result =
                await _departmentService.GetDepartmentByIdAsync(departmentId);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateDepartment(
            UpdateDepartmentRequestDto request)
        {
            var result =
                await _departmentService.UpdateDepartmentAsync(request);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("{departmentId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDepartment(int departmentId)
        {
            var result =
                await _departmentService.DeleteDepartmentAsync(departmentId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchDepartments(
            string? search,
            int pageNumber = 1,
            int pageSize = 10)
        {
            var result =
                await _departmentService.SearchDepartmentsAsync(
                    search,
                    pageNumber,
                    pageSize);

            return Ok(result);
        }
    }
}