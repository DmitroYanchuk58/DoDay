using Data_Access_Layer.Entities;

namespace Business_Logic_Layer.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? Number { get; set; }

        public string? Position { get; set; }

        public byte[]? ProfilePicture { get; set; }

        public UserDTO() { }

        public UserDTO(User user)
        {
            Id = user.Id;
            Username = user.Username;
            Email = user.Email;
            Password = user.Password;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Number = user.Number;
            Position = user.Position;
            ProfilePicture = user.ProfilePicture;
        }

        public User ToEntity()
        {
            if (Number != null && Position != null && ProfilePicture != null)
            {
                return new User(Id, Username, Email, Password, FirstName, LastName, Number, Position, ProfilePicture);
            }
            else
            {
                return new User(Id, Username, Email, Password, FirstName, LastName);
            }
        }
    }
}