using Task = Data_Access_Layer.Entities.Task;

namespace Business_Logic_Layer.DTO
{
    public class TaskDTO
    {
        public Guid? Id { get; set; } = Guid.NewGuid();

        public string? Name { get; set; }

        public DateTime? DateCreated { get; set; } = DateTime.Now;

        public string? Description { get; set; }

        public Status? Status { get; set; }

        public Priority? Priority { get; set; }

        public byte[]? Image { get; set; }

        public TaskDTO() { }

        public TaskDTO(Guid id, string name, DateTime dateCreated, string? description, byte[]? image, string priority, string status) 
        {
            Id = id;
            Name = name;
            DateCreated = dateCreated;
            Description = description;
            Image = image;
            Priority = Enum.TryParse(priority, out Priority parsedPriority) ? parsedPriority : null;
            Status = Enum.TryParse(status, out Status parsedStatus) ? parsedStatus : null;
        }

        public TaskDTO(Task task) : this(task.Id, task.Name, task.DateCreated, task.Description, task.Image, task.Priority, task.Status)
        {
        }

        public Task ToEntity(Guid userId)
        {
            return new Task(
                    Id ?? Guid.NewGuid(),
                    Name ?? "Untitled Task",
                    DateCreated ?? DateTime.Now,
                    Description,
                    Image,
                    Status?.ToString(),
                    Priority?.ToString()
                )
            {
                UserId = userId
            };
        }
    }

    public enum Status 
    { 
        NotStarted, 
        InProgress, 
        Completed, 
        OnHold, 
        Cancelled
    }

    public enum Priority 
    { 
        Low, 
        Medium, 
        High, 
        Urgent
    }
}
