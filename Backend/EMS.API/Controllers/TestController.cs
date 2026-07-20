using Microsoft.AspNetCore.Mvc;
using EMS.API.Data;

namespace EMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly DatabaseConnection _databaseConnection;

        public TestController(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        [HttpGet("testconnection")]
        public IActionResult TestConnection()
        {
            try
            {
                using var connection = _databaseConnection.CreateConnection();

                connection.Open();

                return Ok(new
                {
                    Message = "Database Connected Successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}