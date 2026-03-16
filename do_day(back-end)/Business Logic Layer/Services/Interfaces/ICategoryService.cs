using Business_Logic_Layer.DTO;
using Task = System.Threading.Tasks.Task;

namespace Business_Logic_Layer.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<CategoryDTO> GetCategory(Guid id);

        public Task CreateCategory(CategoryDTO categoryDto);

        public Task ChangeName(Guid id, string newName);

        public Task AddCategoryOption(CategoryDTO categoryDTO, CategoryOptionDTO categoryOptionDTO);

        public Task RemoveCategoryOption(CategoryDTO categoryDTO, CategoryOptionDTO categoryOptionDTO);

        public Task DeleteCategory(Guid id);
    }
}
