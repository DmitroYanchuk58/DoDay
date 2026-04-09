using Business_Logic_Layer.DTO;
using Business_Logic_Layer.Services;
using Data_Access_Layer.Entities;
using Task = System.Threading.Tasks.Task;

namespace Tests.BLL_Tests
{
    public class CategoryOptionServiceTests : ServiceTests
    {
        [Fact]
        [Trait("CategoryOption", "CreateCategoryOption")]
        public async Task CreateCategoryOption_ShouldSaveToDatabase_WhenCategoryExists()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new CategoryOptionService(context);

            var categoryId = Guid.NewGuid();
            var category = new Category
            {
                Id = categoryId,
                Name = "Manga Genres",
                IdUser = Guid.NewGuid()
            };
            context.Categories.Add(category);
            await context.SaveChangesAsync();

            var optionId = Guid.NewGuid();
            var optionDto = new CategoryOptionDTO
            {
                Id = optionId,
                Key = 10,
                Value = "Action",
                CategoryId = categoryId 
            };

            // Act
            await service.CreateCategoryOption(optionDto);

            // Assert
            var optionInDb = context.CategoryOptions.FirstOrDefault(co => co.Id == optionId);

            Assert.NotNull(optionInDb);
            Assert.Equal("Action", optionInDb.Value);
            Assert.Equal(categoryId, optionInDb.CategoryId);
        }

        [Fact]
        [Trait("CategoryOption", "CreateCategoryOption")]
        public async Task CreateCategoryOption_ShouldThrowArgumentNullException_WhenDtoIsNull()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new CategoryOptionService(context);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
                service.CreateCategoryOption(null)
            );

            Assert.Contains("CategoryOptionDTO cannot be null.", exception.Message);
        }

        [Fact]
        [Trait("CategoryOption", "CreateCategoryOption")]
        public async Task CreateCategoryOption_ShouldCorrectlyMapAllFields()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new CategoryOptionService(context);
            var categoryId = Guid.NewGuid();
            var category = new Category
            {
                Id = categoryId,
                Name = "Manga Genres",
                IdUser = Guid.NewGuid()
            };
            context.Categories.Add(category);
            await context.SaveChangesAsync();

            var optionDto = new CategoryOptionDTO
            {
                Id = Guid.NewGuid(),
                Key = 5,
                Value = "Seinen",
                CategoryId = categoryId
            };

            // Act
            await service.CreateCategoryOption(optionDto);

            // Assert
            var result = context.CategoryOptions.FirstOrDefault(co => co.Id == optionDto.Id);
            Assert.NotNull(result);
            Assert.Equal(optionDto.Key, result.Key);
            Assert.Equal(optionDto.Value, result.Value);
            Assert.Equal(optionDto.CategoryId, result.CategoryId);
        }

        [Fact]
        [Trait("CategoryOption", "DeleteCategoryOption")]
        public async Task DeleteCategoryOption_ShouldRemoveFromDatabase_WhenExists()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new CategoryOptionService(context);

            var category = new Category { Id = Guid.NewGuid(), Name = "Genres", IdUser = Guid.NewGuid() };
            var optionId = Guid.NewGuid();
            var option = new CategoryOption
            {
                Id = optionId,
                Value = "Action",
                CategoryId = category.Id,
                Key = 3
            };

            context.Categories.Add(category);
            context.CategoryOptions.Add(option);
            await context.SaveChangesAsync();

            // Act
            await service.DeleteCategoryOption(optionId);

            // Assert
            var deletedOption = await context.CategoryOptions.FindAsync(optionId);
            Assert.Null(deletedOption); 
        }

        [Fact]
        [Trait("CategoryOption", "DeleteCategoryOption")]
        public async Task DeleteCategoryOption_ShouldThrowArgumentNullException_WhenIdIsEmpty()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new CategoryOptionService(context);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                service.DeleteCategoryOption(Guid.Empty)
            );
        }

        [Fact]
        [Trait("CategoryOption", "DeleteCategoryOption")]
        public async Task DeleteCategoryOption_ShouldNotThrow_WhenOptionDoesNotExist()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new CategoryOptionService(context);
            var fakeId = Guid.NewGuid();

            // Act
            var exception = await Record.ExceptionAsync(() =>
                service.DeleteCategoryOption(fakeId)
            );

            // Assert
            Assert.Null(exception); 
        }

        [Fact]
        [Trait("CategoryOption", "GetCategoryOption")]
        public async Task GetCategoryOption_ShouldReturnDto_WhenExists()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new CategoryOptionService(context);

            var categoryId = Guid.NewGuid();
            var optionId = Guid.NewGuid();
            var option = new CategoryOption
            {
                Id = optionId,
                Key = 5,
                Value = "Fantasy",
                CategoryId = categoryId
            };

            context.CategoryOptions.Add(option);
            await context.SaveChangesAsync();

            // Act
            var result = await service.GetCategoryOption(optionId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Fantasy", result.Value);
            Assert.Equal(5, result.Key);
            Assert.Equal(categoryId, result.CategoryId);
        }

        [Fact]
        [Trait("CategoryOption", "GetCategoryOption")]
        public async Task GetCategoryOption_ShouldThrowArgumentNullException_WhenIdIsEmpty()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new CategoryOptionService(context);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                service.GetCategoryOption(Guid.Empty)
            );
        }

        [Fact]
        [Trait("CategoryOption", "GetCategoryOption")]
        public async Task GetCategoryOption_ShouldThrowException_WhenNotFound()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new CategoryOptionService(context);
            var fakeId = Guid.NewGuid();

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                service.GetCategoryOption(fakeId)
            );
        }
    }
}

