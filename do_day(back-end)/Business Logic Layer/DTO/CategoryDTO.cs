using Data_Access_Layer.Entities;
using System.ComponentModel.DataAnnotations;


namespace Business_Logic_Layer.DTO
{
    public class CategoryDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public Guid IdUser { get; set; }

        public List<CategoryOptionDTO>? CategoryOptions { get; set; }

        public CategoryDTO() { }
        public CategoryDTO(Guid id, string name)
        { 
            this.Id = id;
            this.Name = name;
        }

        public CategoryDTO(Guid id, string name,Guid idUser, List<CategoryOptionDTO> categoryOptions) : this(id, name)
        { 
            this.IdUser = idUser;
            this.CategoryOptions = categoryOptions;
        }

        public CategoryDTO(Category category) : this(category.Id, category.Name)
        { 
            this.CategoryOptions = category.CategoryOptions?.Select(co => new CategoryOptionDTO(co)).ToList();
        }
    }
}