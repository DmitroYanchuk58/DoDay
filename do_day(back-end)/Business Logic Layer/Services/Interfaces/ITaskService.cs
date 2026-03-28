using Business_Logic_Layer.DTO;

namespace Business_Logic_Layer.Services.Interfaces
{
    public interface ITaskService
    {
        public Task<TaskDTO> GetTask(Guid id);

        public Task CreateTask(TaskDTO taskDto, Guid idUser);

        public Task ChangeName(Guid id, string newName);

        public Task ChangeDescription(Guid id, string newDescription);

        public Task ChangeImage(Guid id, byte[] newImage);

        public Task DeleteTask(Guid id);

        public Task<List<TaskDTO>> GetTasksByUserId(Guid idUser);
    }
}
