namespace Data_Access_Layer.Entities
{
    public class Category : Entity
    {
        public required string Name { get; set; }    

        public List<CategoryTask> CategoryTasks { get; set; }

        public List<CategoryOption> CategoryOptions { get; set; }
    }
}
