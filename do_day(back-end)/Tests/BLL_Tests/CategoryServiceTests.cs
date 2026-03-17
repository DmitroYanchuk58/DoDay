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
            var category = new Category { Id = categoryId, Name = "Manga" };
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
            var category = new Category { Id = categoryId, Name = "Old Name" };
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
            var category = new Category { Id = categoryId, Name = "ToDelete" };
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
    }
}
