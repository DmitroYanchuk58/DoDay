using Business_Logic_Layer.DTO;

namespace Business_Logic_Layer.Services.Interfaces
{
    public interface IAuthorizationService
    {
        public Task Register(UserDTO user);

        public Task<bool> Login(string email, string password);
    }
}
