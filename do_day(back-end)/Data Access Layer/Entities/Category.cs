namespace Data_Access_Layer.Entities
{
    public class Category : Entity
    {
        public required string Name { get; set; }    

        public required Guid IdUser { get; set; }

        public User User { get; set; }

        public List<CategoryOption> CategoryOptions { get; set; }
    }
}
