using Microsoft.AspNetCore.Mvc;
using Business_Logic_Layer.Services.Interfaces;

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


        [HttpPost("register")]
        public async Task Register(string username, string password, string email, string firstName, string lastName)
        {
            await _service.Register(username, password, email, firstName, lastName);
        }

        [HttpPost("login")]
        public async Task<bool> Login(string email, string password)
        {
            return await _service.Login(email, password);
        }
    }
}
