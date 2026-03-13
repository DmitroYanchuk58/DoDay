using System.ComponentModel.DataAnnotations;
using Data_Access_Layer.Entities;

namespace Business_Logic_Layer.DTO
{
    public class TaskDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(100, ErrorMessage = "Username cannot exceed 100 characters")]
        [MinLength(3, ErrorMessage = "Username is too short")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
        public string Password { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        public string? Number { get; set; }

        public string? Position { get; set; }

        public byte[]? ProfilePicture { get; set; }

        public TaskDto() { }

        public TaskDto(User user)
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