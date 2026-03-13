using Business_Logic_Layer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Logic_Layer.Services.Interfaces
{
    public interface IUserService
    {
        public Task ChangePassword(Guid userId, string oldPassword, string newPassword);

        public Task ChangeNickname(Guid idUser, string newNickname);

        public Task UploadProfilePicture(Guid idUser, byte[] newProfilePicture);

        public Task ChangePosition(Guid idUser, string newPosition);

        public Task ChangeNumber(Guid idUser, string newNumber);

        public Task DeleteUser(Guid idUser);

        public Task<UserDTO> GetUserById(Guid idUser);
    }
}
