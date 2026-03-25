using Task = Data_Access_Layer.Entities.Task;

namespace Business_Logic_Layer.DTO
{
    public class TaskDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public string? Description { get; set; }

        public byte[]? Image { get; set; }

        public TaskDTO() { }

        public TaskDTO(Guid id, string name, DateTime dateCreated, string? description, byte[]? image) 
        {
            Id = id;
            Name = name;
            DateCreated = dateCreated;
            Description = description;
            Image = image;
        }

        public TaskDTO(Task task) : this(task.Id, task.Name, task.DateCreated, task.Description, task.Image)
        {
        }

        public Task ToEntity(Guid userId)
        {
            return new Task(Id, Name, DateCreated, Description, Image)
            {
                UserId = userId
            };
        }

    }
}
