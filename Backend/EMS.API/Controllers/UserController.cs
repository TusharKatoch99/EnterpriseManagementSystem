using System.Security.Claims;
using EMS.API.Interfaces.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {

        private readonly ICurrentUserService _currentUser;

        public UserController(ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
        }

        
        [HttpGet("profile")]
        public IActionResult Profile()
        {
            return Ok("You are authenticated.");
        }



        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok(new
            {
                UserId = _currentUser.UserId,
                Email = _currentUser.Email,
                UserName = _currentUser.UserName,
                FirstName = _currentUser.FirstName,
                LastName = _currentUser.LastName,
                Roles = _currentUser.Roles,
                IsAuthenticated = _currentUser.IsAuthenticated
            });
        }   
    }
}