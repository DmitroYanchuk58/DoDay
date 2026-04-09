using Business_Logic_Layer.DTO;
using Business_Logic_Layer.Services;
using Data_Access_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Tests.BLL_Tests
{
    public class CategoryServiceTests : ServiceTests
    {
        [Fact]
        [Trait("Category", "GetCategory")]
        public async Task GetCategory_ShouldReturnCategoryDTO_WhenExists()
        {
            // Arrange
            using var context = GetDbContext();
            var categoryId = Guid.NewGuid();
            var category = new Category { Id = categoryId, Name = "Manga", IdUser = Guid.NewGuid() };
            context.Categories.Add(category);
            await context.SaveChangesAsync();

            var service = new CategoryService(context);

            // Act
            var result = await service.GetCategory(categoryId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Manga", result.Name);
            Assert.Equal(categoryId, result.Id);
        }

        [Fact]
        [Trait("Category", "GetCategory")]
        public async Task GetCategory_ShouldThrowArgumentException_WhenIdIsEmpty()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new CategoryService(context);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                service.GetCategory(Guid.Empty)
            );

            Assert.Equal("Id cannot be empty.", exception.Message);
        }

        [Fact]
        [Trait("Category", "GetCategory")]
        public async Task GetCategory_ShouldThrowKeyNotFoundException_WhenCategoryDoesNotExist()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new CategoryService(context);
            var fakeId = Guid.NewGuid();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                service.GetCategory(fakeId)
            );

            Assert.Contains(fakeId.ToString(), exception.Message);
        }

        [Fact]
        [Trait("Category", "GetCategory")]
        public async Task GetCategory_ShouldIncludeOptions_InResultDTO()
        {
            // Arrange
            using var context = GetDbContext();
            var categoryId = Guid.NewGuid();
            var category = new Category
            {
                Id = categoryId,
                Name = "Genre",
                IdUser = Guid.NewGuid(),
                CategoryOptions = new List<CategoryOption>
        {
            new CategoryOption { Id = Guid.NewGuid(), Key = 1, Value = "action" }
        }
            };
            context.Categories.Add(category);
            await context.SaveChangesAsync();

            var service = new CategoryService(context);

            // Act
            var result = await service.GetCategory(categoryId);

            // Assert
            Assert.Single(result.CategoryOptions);
            Assert.Equal("action", result.CategoryOptions.First().Value);
        }

        [Fact]
        [Trait("Category", "CreateCategory")]
        public async Task CreateCategory_ShouldSaveToDatabase_WhenDataIsValid()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new CategoryService(context);
            var categoryDto = new CategoryDTO
            {
                Id = Guid.NewGuid(),
                Name = "Manga Genres"
            };

            // Act
            await service.CreateCategory(categoryDto);

            // Assert
            var categoryInDb = await context.Categories.FirstOrDefaultAsync(c => c.Name == "Manga Genres");
            Assert.NotNull(categoryInDb);
            Assert.Equal(categoryDto.Id, categoryInDb.Id);
        }

        [Fact]
        [Trait("Category", "CreateCategory")]
        public async Task CreateCategory_ShouldThrowArgumentNullException_WhenDtoIsNull()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new CategoryService(context);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => service.CreateCategory(null));
        }

        [Fact]
        [Trait("Category", "CreateCategory")]
        public async Task CreateCategory_ShouldCorrectlyMapCategoryOptions()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new CategoryService(context);
            var categoryDto = new CategoryDTO
            {
                Id = Guid.NewGuid(),
                Name = "Format",
                CategoryOptions = new List<CategoryOptionDTO>
                {
                    new CategoryOptionDTO { Id = Guid.NewGuid(), Key = 10, Value = "Digital" },
                    new CategoryOptionDTO { Id = Guid.NewGuid(), Key = 20, Value = "Physical" }
                }
            };

            // Act
            await service.CreateCategory(categoryDto);

            // Assert
            var categoryInDb = await context.Categories
                .Include(c => c.CategoryOptions)
                .FirstOrDefaultAsync(c => c.Id == categoryDto.Id);

            Assert.NotNull(categoryInDb);
            Assert.Equal(2, categoryInDb.CategoryOptions.Count);
            Assert.Contains(categoryInDb.CategoryOptions, co => co.Value == "Digital" && co.Key == 10);
            Assert.Contains(categoryInDb.CategoryOptions, co => co.Value == "Physical" && co.Key == 20);
        }

        [Fact]
        [Trait("Category", "CreateCategory")]
        public async Task CreateCategory_ShouldWork_WhenCategoryOptionsAreNull()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new CategoryService(context);
            var categoryDto = new CategoryDTO
            {
                Id = Guid.NewGuid(),
                Name = "Empty Category",
                CategoryOptions = null 
            };

            // Act
            await service.CreateCategory(categoryDto);

            // Assert
            var categoryInDb = await context.Categories.FindAsync(categoryDto.Id);
            Assert.NotNull(categoryInDb);
            Assert.Null(categoryInDb.CategoryOptions);
        }

        [Fact]
        [Trait("Category", "ChangeName")]
        public async Task ChangeName_ShouldUpdateNameInDatabase_WhenDataIsValid()
        {
            // Arrange
            using var context = GetDbContext();
            var categoryId = Guid.NewGuid();
            var category = new Category { Id = categoryId, Name = "Old Name", IdUser = Guid.NewGuid() };
            context.Categories.Add(category);
            await context.SaveChangesAsync();

            var service = new CategoryService(context);
            string newName = "New Manga Name";

            // Act
            await service.ChangeName(categoryId, newName);

            // Assert
            var updatedCategory = await context.Categories.FindAsync(categoryId);
            Assert.Equal(newName, updatedCategory.Name);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(null)]
        [Trait("Category", "ChangeName")]
        public async Task ChangeName_ShouldThrowArgumentException_WhenNameIsInvalid(string invalidName)
        {
            // Arrange
            using var context = GetDbContext();
            var service = new CategoryService(context);
            var categoryId = Guid.NewGuid();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                service.ChangeName(categoryId, invalidName)
            );

            Assert.Equal("New name cannot be null or whitespace.", exception.Message);
        }

        [Fact]
        [Trait("Category", "ChangeName")]
        public async Task ChangeName_ShouldThrowArgumentException_WhenIdIsEmpty()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new CategoryService(context);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() =>
                service.ChangeName(Guid.Empty, "Valid Name")
            );
        }

        [Fact]
        [Trait("Category", "ChangeName")]
        public async Task ChangeName_ShouldThrowKeyNotFoundException_WhenCategoryDoesNotExist()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new CategoryService(context);
            var fakeId = Guid.NewGuid();

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                service.ChangeName(fakeId, "New Name")
            );
        }

        [Fact]
        [Trait("Category", "DeleteCategory")]
        public async Task DeleteCategory_ShouldRemoveCategory_WhenCategoryExists()
        {
            // Arrange
            using var context = GetDbContext();
            var categoryId = Guid.NewGuid();
            var category = new Category { Id = categoryId, Name = "ToDelete", IdUser = Guid.NewGuid() };
            context.Categories.Add(category);
            await context.SaveChangesAsync();

            var service = new CategoryService(context);

            // Act
            await service.DeleteCategory(categoryId);

            // Assert
            var categoryInDb = await context.Categories.FindAsync(categoryId);
            Assert.Null(categoryInDb); 
        }

        [Fact]
        [Trait("Category", "DeleteCategory")]
        public async Task DeleteCategory_ShouldThrowArgumentException_WhenIdIsEmpty()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new CategoryService(context);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                service.DeleteCategory(Guid.Empty)
            );

            Assert.Equal("Id cannot be empty.", exception.Message);
        }

        [Fact]
        [Trait("Category", "DeleteCategory")]
        public async Task DeleteCategory_ShouldNotThrow_WhenCategoryDoesNotExist()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new CategoryService(context);
            var fakeId = Guid.NewGuid();

            // Act
            var exception = await Record.ExceptionAsync(() =>
                service.DeleteCategory(fakeId)
            );

            // Assert
            Assert.Null(exception); 
        }
        [Fact]
        [Trait("Category", "AddCategoryOptionToCategory")]
        public async Task AddCategoryOptionToCategory_ShouldSetCategoryIdAndAddToList()
        {
            // Arrange
            using var context = GetDbContext();
            var categoryService = new CategoryService(context);
            var optionService = new CategoryOptionService(context);

            var categoryId = Guid.NewGuid();
            var categoryDto = new CategoryDTO { Id = categoryId, Name = "Manga" };
            var categoryOptionId = Guid.NewGuid();

            var optionDto = new CategoryOptionDTO
            {
                Id = categoryOptionId,
                Key = 1,
                Value = "Digital"
            };

            // Act
            var result = await categoryService.AddCategoryOptionToCategory(categoryDto, optionDto, optionService);

            // Assert
            Assert.Equal(categoryId, optionDto.CategoryId);

            Assert.NotNull(result.CategoryOptions);
            Assert.Single(result.CategoryOptions);
            Assert.Contains(result.CategoryOptions, o => o.Id == categoryOptionId);

            var optionInDb = await context.CategoryOptions.FindAsync(categoryOptionId);
            Assert.NotNull(optionInDb);
            Assert.Equal(categoryId, optionInDb.CategoryId);
        }

        [Fact]
        [Trait("Category", "AddCategoryOptionToCategory")]
        public async Task AddCategoryOptionToCategory_ShouldWork_WhenListStartsAsNull()
        {
            // Arrange
            using var context = GetDbContext();
            var categoryService = new CategoryService(context);
            var optionService = new CategoryOptionService(context);

            var categoryDto = new CategoryDTO { Id = Guid.NewGuid(), CategoryOptions = null };
            var optionDto = new CategoryOptionDTO { Id = Guid.NewGuid(), Key = 2, Value = "Important" };

            // Act
            var result = await categoryService.AddCategoryOptionToCategory(categoryDto, optionDto, optionService);

            // Assert
            Assert.NotNull(result.CategoryOptions);
            Assert.Single(result.CategoryOptions);
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [Trait("Category", "AddCategoryOptionToCategory")]
        public async Task AddCategoryOptionToCategory_ShouldThrowException_WhenArgumentsAreNull(bool catIsNull, bool optIsNull)
        {
            // Arrange
            using var context = GetDbContext();
            var categoryService = new CategoryService(context);
            var optionService = new CategoryOptionService(context);

            var category = catIsNull ? null : new CategoryDTO();
            var option = optIsNull ? null : new CategoryOptionDTO();

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                categoryService.AddCategoryOptionToCategory(category, option, optionService)
            );
        }

        [Fact]
        [Trait("Category", "RemoveCategoryOptionFromCategory")]
        public async Task RemoveCategoryOptionFromCategory_ShouldRemoveFromListAndDatabase()
        {
            // Arrange
            using var context = GetDbContext();
            var categoryService = new CategoryService(context);
            var optionService = new CategoryOptionService(context);

            var optionId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();

            var categoryEntity = new Category { Id = categoryId, Name = "Genres", IdUser = Guid.NewGuid() };
            var optionEntity = new CategoryOption { Id = optionId, CategoryId = categoryId, Value = "Action", Key = 10 };

            context.Categories.Add(categoryEntity);
            context.CategoryOptions.Add(optionEntity);
            await context.SaveChangesAsync();

            var categoryDto = new CategoryDTO
            {
                Id = categoryId,
                CategoryOptions = new List<CategoryOptionDTO>
                {
                    new CategoryOptionDTO { Id = optionId }
                }
            };
            var optionDto = new CategoryOptionDTO { Id = optionId };

            // Act
            var result = await categoryService.RemoveCategoryOptionFromCategory(categoryDto, optionDto, optionService);

            // Assert
            Assert.Empty(result.CategoryOptions);

            var optionInDb = await context.CategoryOptions.FindAsync(optionId);
            Assert.Null(optionInDb);
        }

        [Fact]
        [Trait("Category", "RemoveCategoryOptionFromCategory")]
        public async Task RemoveCategoryOptionFromCategory_ShouldNotThrow_WhenListIsNull()
        {
            // Arrange
            using var context = GetDbContext();
            var categoryService = new CategoryService(context);
            var optionService = new CategoryOptionService(context);

            var categoryDto = new CategoryDTO { Id = Guid.NewGuid(), CategoryOptions = null };
            var optionDto = new CategoryOptionDTO { Id = Guid.NewGuid() };

            // Act
            var exception = await Record.ExceptionAsync(() =>
                categoryService.RemoveCategoryOptionFromCategory(categoryDto, optionDto, optionService)
            );

            // Assert
            Assert.Null(exception);
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData(false, true)]
        [Trait("Category", "RemoveCategoryOptionFromCategory")]
        public async Task RemoveCategoryOptionFromCategory_ShouldThrowException_WhenArgumentsAreNull(bool catIsNull, bool optIsNull)
        {
            // Arrange
            using var context = GetDbContext();
            var categoryService = new CategoryService(context);
            var optionService = new CategoryOptionService(context);

            var category = catIsNull ? null : new CategoryDTO();
            var option = optIsNull ? null : new CategoryOptionDTO();

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                categoryService.RemoveCategoryOptionFromCategory(category, option, optionService)
            );
        }
    }
}
