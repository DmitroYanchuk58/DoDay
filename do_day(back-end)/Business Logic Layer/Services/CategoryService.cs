using Business_Logic_Layer.DTO;
using Business_Logic_Layer.Services.Interfaces;
using Business_Logic_Layer.Validators;
using Data_Access_Layer.DatabaseContext;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Repositories;
using Task = System.Threading.Tasks.Task;

namespace Business_Logic_Layer.Services
{
    public class CategoryService : ICategoryService
    {
        CrudRepository<Category> _categoryRepository;

        public CategoryService(DoDayDBContext context)
        {
            _categoryRepository = new CrudRepository<Category>(context);
        }

        public async Task<CategoryDTO> GetCategory(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id cannot be empty.");
            }
            var categoryEntity = await _categoryRepository.GetByIdAsync(id);
            if (categoryEntity == null)
            {
                throw new KeyNotFoundException($"Category with id {id} was not found.");
            }
            return new CategoryDTO(categoryEntity);
        }

        public async Task<List<CategoryDTO>> GetAllUserCategories(Guid idUser)
        {
            var categoryEntities = (await _categoryRepository.GetAllAsync()).Where(c => c.IdUser == idUser).ToList();
            return categoryEntities.Select(c => new CategoryDTO(c)).ToList();
        }

        public async Task CreateCategory(CategoryDTO categoryDto)
        {
            if (categoryDto == null)
            {
                throw new ArgumentNullException(nameof(categoryDto), "CategoryDTO cannot be null.");
            }
            var categoryEntity = new Category()
            {
                Id = categoryDto.Id,
                Name = categoryDto.Name,
                IdUser = categoryDto.IdUser,
                CategoryOptions = categoryDto.CategoryOptions?.Select(co => new CategoryOption
                {
                    Id = co.Id,
                    Key = co.Key,
                    Value = co.Value
                }).ToList()
            };
            CategoryDTOValidator validator = new CategoryDTOValidator();
            var result = validator.Validate(categoryDto);
            if (!result.IsValid)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                throw new ArgumentException(errors);
            }
            await _categoryRepository.CreateAsync(categoryEntity);
        }

        public async Task<CategoryDTO> AddCategoryOptionToCategory(CategoryDTO category,CategoryOptionDTO categoryOption, ICategoryOptionService categoryOptionService)
        {
            if(categoryOption == null)
            {
                throw new ArgumentNullException(nameof(categoryOption), "CategoryOptionDTO cannot be null.");
            }
            if(category == null)
            {
                throw new ArgumentNullException(nameof(category), "CategoryDTO cannot be null.");
            }
            categoryOption.CategoryId = category.Id;
            await categoryOptionService.CreateCategoryOption(categoryOption);
            category.CategoryOptions ??= new List<CategoryOptionDTO>();
            category.CategoryOptions.Add(categoryOption);
            return category;
        }

        public async Task<CategoryDTO> RemoveCategoryOptionFromCategory(CategoryDTO category, CategoryOptionDTO categoryOption, ICategoryOptionService categoryOptionService)
        {
            if (categoryOption == null)
            {
                throw new ArgumentNullException(nameof(categoryOption), "CategoryOptionDTO cannot be null.");
            }
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "CategoryDTO cannot be null.");
            }
            await categoryOptionService.DeleteCategoryOption(categoryOption.Id);
            category.CategoryOptions?.RemoveAll(co => co.Id == categoryOption.Id);
            return category;
        }

        public async Task UpdateCategory(CategoryDTO category)
        {
            if (category.Id == Guid.Empty)
            {
                throw new ArgumentException("Id cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(category.Name))
            {
                throw new ArgumentException("New name cannot be null or whitespace.");
            }
            var categoryEntity = await _categoryRepository.GetByIdAsync(category.Id);
            if (categoryEntity == null)
            {
                throw new KeyNotFoundException($"Category with id {category.Id} was not found.");
            }
            categoryEntity.Name = category.Name;
            await _categoryRepository.UpdateAsync(categoryEntity);
        }

        public async Task DeleteCategory(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id cannot be empty.");
            }
            await _categoryRepository.DeleteAsync(id);
        }
    }
}
