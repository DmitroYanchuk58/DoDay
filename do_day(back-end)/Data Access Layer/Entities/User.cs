using System.Diagnostics.CodeAnalysis;

namespace Data_Access_Layer.Entities
{
    public class User : Entity
    {
        #region Important Datas
        public required string Username { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }


        #endregion

        #region Additional Data

        public required string FirstName { get; set; }  

        public required string LastName { get; set; }

        public string? Number { get; set; } 

        public string? Position { get; set; }   

        public byte[]? ProfilePicture { get; set; }

        #endregion

        #region Relaptionships

        public List<Task> Tasks { get; set; } = new List<Task>();

        public List<Category> Categories { get; set; } = new List<Category>();

        #endregion

        #region Constructor

        [SetsRequiredMembers]
        public User(Guid id, 
            string username, string email, string password,
            string firstName, string lastName,
            string? number, string? position, byte[]? profilePicture)
        {

            this.Id = id;
            this.Username = username;
            this.Email = email;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Number = number;
            this.Position = position;
            this.ProfilePicture = profilePicture;
        }

        [SetsRequiredMembers]
        public User(Guid id,
            string username, string email, string password,
            string firstName, string lastName) 
            : 
            this(id,username,email,password,firstName, lastName,    
                null,null, null)
        {
        }

        #endregion
    }
}
