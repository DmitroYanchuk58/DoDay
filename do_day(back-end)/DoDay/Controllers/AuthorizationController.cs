using Azure.Core;
using Business_Logic_Layer.DTO;
using Business_Logic_Layer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using API_Layer.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
            var createdUser = await _service.Register(user);

            var token = GenerateJwtToken(createdUser);

            return Ok(new AuthResponse
            {
                User = createdUser,
                Token = token,
                IsSuccess = true
            });
        }

        [HttpGet("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var (isSuccess, user) = await _service.Login(email, password);

            if (!isSuccess)
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            var token = GenerateJwtToken(user);

            return Ok(new AuthResponse
            {
                User = user,
                Token = token,
                IsSuccess = true
            });
        }


        private string GenerateJwtToken(UserDTO user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("DoDay_Super_Secret_Security_Key_2026_Secure_Version"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(7); 

            var token = new JwtSecurityToken(
                issuer: "DoDayAPI",
                audience: "DoDayUsers",
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
