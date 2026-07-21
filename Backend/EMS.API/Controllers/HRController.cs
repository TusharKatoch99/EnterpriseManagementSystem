using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,HR")]
    public class HRController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Accessible by Admin or HR.");
        }
    }
}