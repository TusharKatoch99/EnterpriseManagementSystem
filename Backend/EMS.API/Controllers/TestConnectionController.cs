using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace EMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestConnectionController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TestConnectionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                return Ok("✅ SQL Server Connected Successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}