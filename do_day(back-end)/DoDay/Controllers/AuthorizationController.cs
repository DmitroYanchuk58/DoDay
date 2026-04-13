using Microsoft.AspNetCore.Mvc;
using Business_Logic_Layer.Services.Interfaces;
using Business_Logic_Layer.DTO;

namespace API_Layer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private IAuthorizationService _service;

        public AuthorizationController(IAuthorizationService service)
        {
            _service = service;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserDTO user)
        {
            user = await _service.Register(user);
            return Ok(user);
        }

        [HttpGet("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var (isSuccess, user) = await _service.Login(email, password);

            if (!isSuccess)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            return Ok(new
            {
                isSuccess = isSuccess,
                user = user
            });
        }
    }
}
