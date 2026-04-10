using Business_Logic_Layer.DTO;
using Business_Logic_Layer.Validators;
using Data_Access_Layer.Entities;
using Task = System.Threading.Tasks.Task;

namespace Business_Logic_Layer.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<CategoryDTO> GetCategory(Guid id);

        public Task<List<CategoryDTO>> GetAllUserCategories(Guid idUser);

        public Task CreateCategory(CategoryDTO categoryDto);

        public Task UpdateCategory(CategoryDTO category);

        public Task DeleteCategory(Guid id);
    }
}
