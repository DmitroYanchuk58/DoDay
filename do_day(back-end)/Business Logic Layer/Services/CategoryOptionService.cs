using Business_Logic_Layer.DTO;
using Data_Access_Layer.DatabaseContext;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Repositories;
using Task = System.Threading.Tasks.Task;

namespace Business_Logic_Layer.Services
{
    public class CategoryOptionService
    {
        CrudRepository<CategoryOption> _categoryOptionRepository;

        public CategoryOptionService(DoDayDBContext context)
        {
            _categoryOptionRepository = new CrudRepository<CategoryOption>(context);
        }

        public async Task CreateCategoryOption(CategoryOptionDTO categoryOptionDTO)
        {
            if (categoryOptionDTO == null)
            {
                throw new ArgumentNullException(nameof(categoryOptionDTO), "CategoryOptionDTO cannot be null.");
            }
            CategoryOption categoryOption = new CategoryOption() 
            { 
                Id = categoryOptionDTO.Id,
                Key = categoryOptionDTO.Key,
                Value = categoryOptionDTO.Value
            };
            await _categoryOptionRepository.CreateAsync(categoryOption);
        }

        public async Task DeleteCategoryOption(Guid id)
        {
            if(id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }
            await _categoryOptionRepository.DeleteAsync(id);
        }
    }
}
