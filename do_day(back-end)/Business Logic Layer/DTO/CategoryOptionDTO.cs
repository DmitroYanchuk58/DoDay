using Data_Access_Layer.Entities;
using System.ComponentModel.DataAnnotations;

namespace Business_Logic_Layer.DTO
{
    public class CategoryOptionDTO
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Range(1,100)]
        public int Key { get; set; }

        [Required]
        [MaxLength(200), MinLength(1)]
        public string Value { get; set; }

        public CategoryOptionDTO(Guid id, int key, string value)
        { 
            this.Id = id;
            this.Key = key;
            this.Value = value;
        }

        public CategoryOptionDTO(CategoryOption categoryOption) : this(categoryOption.Id, categoryOption.Key, categoryOption.Value)
        {
        }
    }
}
