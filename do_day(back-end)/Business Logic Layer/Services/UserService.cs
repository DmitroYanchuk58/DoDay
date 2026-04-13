using Business_Logic_Layer.DTO;
using Business_Logic_Layer.Services.Interfaces;
using Business_Logic_Layer.Validation;
using Data_Access_Layer.DatabaseContext;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Repositories;
using Task = System.Threading.Tasks.Task;

namespace Business_Logic_Layer.Services
{
    public class UserService : IAuthorizationService, IUserService
    {
        CrudRepository<User> _userRepository;

        public UserService(DoDayDBContext context)
        {
            _userRepository = new CrudRepository<User>(context);
        }

        public async Task<(bool,UserDTO)> Login(string email, string password)
        {
            var users = await _userRepository.GetAllAsync();
            User? findUser;
            try
            {
                findUser = users.First(u => string.Equals(u.Email.ToLower(), email.ToLower()));
            }
            catch (Exception ex)
            {
                throw new ArgumentException("User doesn't exist", ex);
            }
            if (findUser == null)
            {
                throw new Exception("User not found");
            }
            if (string.Equals(findUser.Password, password))
            {
                var userDto = new UserDTO(findUser);
                return (true, userDto);
            }
            return (false,null!);
        }

        public async Task<UserDTO> Register(UserDTO userDto)
        {
            var validator = new UserDTOValidator();
            var result = validator.Validate(userDto);
            if (!result.IsValid)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                throw new ArgumentException(errors);
            }

            var user = userDto.ToEntity();

            await _userRepository.CreateAsync(user);
            
            return new UserDTO(user);
        }

        public async Task ChangePassword(Guid userId, string oldPassword, string newPassword)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null) throw new Exception("User not found");

            ArgumentNullException.ThrowIfNullOrWhiteSpace(newPassword, nameof(newPassword));

            if (newPassword.Length < 8 || newPassword.Length > 100)
            {
                throw new Exception("New password must be between 8 and 20 characters");
            }

            if (!string.Equals(user.Password, oldPassword))
            {
                throw new Exception("Old password is incorrect");
            }

            user.Password = newPassword;
            await _userRepository.UpdateAsync(user);
        }

        public async Task ChangeNickname(Guid idUser, string newNickname)
        {
            var user = await _userRepository.GetByIdAsync(idUser);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            ArgumentNullException.ThrowIfNullOrWhiteSpace(newNickname, nameof(newNickname));
            if (newNickname.Length < 3 || newNickname.Length > 100)
            {
                throw new Exception("New nickname must be between 8 and 20 characters");
            }
            user.Username = newNickname;
            await _userRepository.UpdateAsync(user);
        }

        public async Task UpdateUser(UserDTO userDTO)
        {
            var user = await _userRepository.GetByIdAsync(userDTO.Id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            user.FirstName = !string.IsNullOrWhiteSpace(userDTO.FirstName) ? userDTO.FirstName : user.FirstName;
            user.LastName = !string.IsNullOrWhiteSpace(userDTO.LastName) ? userDTO.LastName : user.LastName;
            user.Email = !string.IsNullOrWhiteSpace(userDTO.Email) ? userDTO.Email : user.Email;
            user.Position = !string.IsNullOrWhiteSpace(userDTO.Position) ? userDTO.Position : user.Position;
            user.Number = !string.IsNullOrWhiteSpace(userDTO.Number) ? userDTO.Number : user.Number;
            await _userRepository.UpdateAsync(user);
        }

        public async Task UploadProfilePicture(Guid idUser, byte[] newProfilePicture)
        {
            var user = await _userRepository.GetByIdAsync(idUser);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            user.ProfilePicture = newProfilePicture;
            await _userRepository.UpdateAsync(user);
        }

        public async Task ChangePosition(Guid idUser, string newPosition)
        {
            var user = await _userRepository.GetByIdAsync(idUser);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var userDto = new UserDTO(user);
            userDto.Position = newPosition;
            user.Position = userDto.Position;
            await _userRepository.UpdateAsync(user);
        }

        public async Task ChangeNumber(Guid idUser, string newNumber)
        {
            var user = await _userRepository.GetByIdAsync(idUser);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var userDto = new UserDTO(user);
            userDto.Number = newNumber;
            user.Number = userDto.Number;
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteUser(Guid idUser)
        {
            await _userRepository.DeleteAsync(idUser);
        }

        public async Task<UserDTO> GetUserById(Guid idUser)
        {            
            var user = await _userRepository.GetByIdAsync(idUser);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            return new UserDTO(user);
        }
    }
}
