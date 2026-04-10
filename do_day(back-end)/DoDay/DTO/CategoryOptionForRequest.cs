namespace API_Layer.DTO
{
    public class CategoryOptionForRequest
    {
        public Guid Id {  get; set; }

        public int Key { get; set; }

        public string Value { get; set; }

        public Guid IdCategory {  get; set; }
    }
}
