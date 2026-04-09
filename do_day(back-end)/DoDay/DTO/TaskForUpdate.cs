namespace API_Layer.DTO
{
    public class TaskForUpdate
    {
        public Guid? Id { get; set; }

        public string? Name { get; set; }

        public DateTime? DateCreated { get; set; }

        public string? Description { get; set; }

        public byte[]? Image { get; set; }
    }
}
