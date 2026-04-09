using Business_Logic_Layer.DTO;
using Business_Logic_Layer.Validators;
using Data_Access_Layer.Entities;
using Task = System.Threading.Tasks.Task;

namespace Business_Logic_Layer.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<CategoryDTO> GetCategory(Guid id);

        public Task CreateCategory(CategoryDTO categoryDto);

        public Task<CategoryDTO> AddCategoryOptionToCategory(CategoryDTO category, CategoryOptionDTO categoryOption, ICategoryOptionService categoryOptionService);

        public Task<CategoryDTO> RemoveCategoryOptionFromCategory(CategoryDTO category, CategoryOptionDTO categoryOption, ICategoryOptionService categoryOptionService);

        public Task ChangeName(Guid id, string newName);

        public Task DeleteCategory(Guid id);
    }
}
