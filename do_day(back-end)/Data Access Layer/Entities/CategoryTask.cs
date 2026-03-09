namespace Data_Access_Layer.Entities
{
    public class CategoryTask : Entity
    {
        public Guid CategoryId { get; set; }

        public Category Category { get; set; }

        public Guid TaskId { get; set; }

        public Task Task { get; set; }
    }
}
