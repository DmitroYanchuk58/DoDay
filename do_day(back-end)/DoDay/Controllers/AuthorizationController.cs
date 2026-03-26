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
        public async Task<UserDTO> Register(UserDTO user)
        {
            await _service.Register(user);
            return user;
        }

        [HttpGet("Login")]
        public async Task<bool> Login(string email, string password)
        {
            return await _service.Login(email, password);
        }
    }
}
