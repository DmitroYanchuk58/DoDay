using Data_Access_Layer.Entities;
using System.ComponentModel.DataAnnotations;


namespace Business_Logic_Layer.DTO
{
    public class CategoryDTO
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(200), MinLength(1)]
        public string Name { get; set; }

        public List<TaskDTO>? Tasks{ get; set;  }

        public List<CategoryOptionDTO>? CategoryOptions { get; set; }

        public CategoryDTO() { }
        public CategoryDTO(Guid id, string name)
        { 
            this.Id = id;
            this.Name = name;
        }

        public CategoryDTO(Guid id, string name, List<TaskDTO> tasks, List<CategoryOptionDTO> categoryOptions) : this(id, name)
        { 
            this.Tasks = tasks;
            this.CategoryOptions = categoryOptions;
        }

        public CategoryDTO(Category category) : this(category.Id, category.Name)
        { 
            this.CategoryOptions = category.CategoryOptions?.Select(co => new CategoryOptionDTO(co)).ToList();
            //TODO: add tasks to categoryDTO
        }
    }
}