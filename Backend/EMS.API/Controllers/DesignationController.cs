using EMS.API.DTOs.Designation;
using EMS.API.Interfaces.Designation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DesignationController : ControllerBase
    {
        private readonly IDesignationService _designationService;

        public DesignationController(IDesignationService designationService)
        {
            _designationService = designationService;
        }

        #region Create Designation

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateDesignation(
            CreateDesignationRequestDto request)
        {
            var response = await _designationService.CreateDesignationAsync(request);

            return Ok(response);
        }

        #endregion

        #region Get All Designations

        [HttpGet]
        public async Task<IActionResult> GetDesignations()
        {
            var response = await _designationService.GetDesignationsAsync();

            return Ok(response);
        }

        #endregion

        #region Get Designation By Id

        [HttpGet("{designationId:int}")]
        public async Task<IActionResult> GetDesignationById(int designationId)
        {
            var response = await _designationService.GetDesignationByIdAsync(designationId);

            return Ok(response);
        }

        #endregion

        #region Update Designation

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateDesignation(
            UpdateDesignationRequestDto request)
        {
            var response = await _designationService.UpdateDesignationAsync(request);

            return Ok(response);
        }

        #endregion

        #region Delete Designation

        [HttpDelete("{designationId:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDesignation(int designationId)
        {
            var response = await _designationService.DeleteDesignationAsync(designationId);

            return Ok(response);
        }

        #endregion

        #region Search Designations

        [HttpGet("search")]
        public async Task<IActionResult> SearchDesignations(
            [FromQuery] DesignationSearchRequestDto request)
        {
            var response = await _designationService.SearchDesignationsAsync(request);

            return Ok(response);
        }

        #endregion
    }
}