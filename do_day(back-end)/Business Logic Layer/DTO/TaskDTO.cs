using System.ComponentModel.DataAnnotations;
using Task = Data_Access_Layer.Entities.Task;

namespace Business_Logic_Layer.DTO
{
    public class TaskDTO
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "Task name cannot exceed 500 characters")]
        [MinLength(1, ErrorMessage = "Task name cannot be empty")]
        public string Name { get; set; }

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [MinLength(5, ErrorMessage = "Task description cannot be less than 5 characters")]
        [MaxLength(3000, ErrorMessage = "Task description cannot exceed 3000 characters")]
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
