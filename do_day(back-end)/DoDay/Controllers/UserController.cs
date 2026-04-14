using Business_Logic_Layer.DTO;
using Business_Logic_Layer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(string oldPassword,string newPassword, Guid idUser)
        {
            await _service.ChangePassword(idUser, oldPassword, newPassword);
            return Ok();
        }

        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserDTO user)
        {
            await _service.UpdateUser(user);
            return Ok();
        }
    }
}
