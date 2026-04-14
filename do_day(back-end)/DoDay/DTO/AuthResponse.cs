using Business_Logic_Layer.DTO;

namespace API_Layer.DTO
{
    public class AuthResponse
    {
        public UserDTO User { get; set; }
        public string Token { get; set; } 
    }
}
