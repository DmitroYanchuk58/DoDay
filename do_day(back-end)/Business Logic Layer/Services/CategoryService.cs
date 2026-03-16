using Business_Logic_Layer.DTO;
using Data_Access_Layer.DatabaseContext;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Task = System.Threading.Tasks.Task;

namespace Business_Logic_Layer.Services
{
    public class CategoryService
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
                CategoryOptions = categoryDto.CategoryOptions?.Select(co => new CategoryOption
                {
                    Id = co.Id,
                    Key = co.Key,
                    Value = co.Value
                }).ToList()
            };
            await _categoryRepository.CreateAsync(categoryEntity);
        }

        public async Task ChangeName(Guid id, string newName)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new ArgumentException("New name cannot be null or whitespace.");
            }
            var categoryEntity = await _categoryRepository.GetByIdAsync(id);
            if (categoryEntity == null)
            {
                throw new KeyNotFoundException($"Category with id {id} was not found.");
            }
            var categoryDto = new CategoryDTO(categoryEntity);
            categoryDto.Name = newName;
            categoryEntity.Name = categoryDto.Name;
            await _categoryRepository.UpdateAsync(categoryEntity);
        }

        public async Task AddCategoryOption(CategoryDTO categoryDTO, CategoryOptionDTO categoryOptionDTO)
        {
            if (categoryDTO == null)
            {
                throw new ArgumentNullException(nameof(categoryDTO), "CategoryDTO cannot be null.");
            }
            if (categoryOptionDTO == null)
            {
                throw new ArgumentNullException(nameof(categoryOptionDTO), "CategoryOptionDTO cannot be null.");
            }
            var categoryEntity = await _categoryRepository.GetByIdAsync(categoryDTO.Id);
            if (categoryEntity == null)
            {
                throw new KeyNotFoundException($"Category with id {categoryDTO.Id} was not found.");
            }
            var categoryOptionEntity = new CategoryOption()
            {
                Id = categoryOptionDTO.Id,
                Key = categoryOptionDTO.Key,
                Value = categoryOptionDTO.Value
            };
            categoryEntity.CategoryOptions.Add(categoryOptionEntity);
            await _categoryRepository.UpdateAsync(categoryEntity);
        }

        public async Task RemoveCategoryOption(CategoryDTO categoryDTO, CategoryOptionDTO categoryOptionDTO)
        {
            if (categoryDTO == null)
            {
                throw new ArgumentNullException(nameof(categoryDTO), "CategoryDTO cannot be null.");
            }
            if (categoryOptionDTO == null)
            {
                throw new ArgumentNullException(nameof(categoryOptionDTO), "CategoryOptionDTO cannot be null.");
            }
            var categoryEntity = await _categoryRepository.GetByIdAsync(categoryDTO.Id);
            if (categoryEntity == null)
            {
                throw new KeyNotFoundException($"Category with id {categoryDTO.Id} was not found.");
            }
            var categoryOptionEntity = categoryEntity.CategoryOptions.FirstOrDefault(co => co.Id == categoryOptionDTO.Id);
            if (categoryOptionEntity == null)
            {
                throw new KeyNotFoundException($"Category option with id {categoryOptionDTO.Id} was not found in category with id {categoryDTO.Id}.");
            }
            categoryEntity.CategoryOptions.Remove(categoryOptionEntity);
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
