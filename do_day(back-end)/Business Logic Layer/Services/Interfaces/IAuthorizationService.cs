using Business_Logic_Layer.DTO;

namespace Business_Logic_Layer.Services.Interfaces
{
    public interface IAuthorizationService
    {
        public Task<UserDTO> Register(UserDTO user);

        public Task<(bool, UserDTO)> Login(string email, string password);
    }
}
