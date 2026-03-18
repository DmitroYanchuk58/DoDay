using Business_Logic_Layer.DTO;
using Business_Logic_Layer.Services;
using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Task = System.Threading.Tasks.Task;

namespace Tests.BLL_Tests
{
    public class CategoryOptionServiceTests : ServiceTests
    {
        [Fact]
        [Trait("Category", "CreateCategoryOption")]
        public async Task CreateCategoryOption_ShouldSaveToDatabase_WhenCategoryExists()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new CategoryOptionService(context);

            var categoryId = Guid.NewGuid();
            var category = new Category
            {
                Id = categoryId,
                Name = "Manga Genres"
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
            var optionInDb = context.CategoryOpinions.FirstOrDefault(co => co.Id == optionId);

            Assert.NotNull(optionInDb);
            Assert.Equal("Action", optionInDb.Value);
            Assert.Equal(categoryId, optionInDb.CategoryId);
        }

        [Fact]
        [Trait("Category", "CreateCategoryOption")]
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
        [Trait("Category", "CreateCategoryOption")]
        public async Task CreateCategoryOption_ShouldCorrectlyMapAllFields()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new CategoryOptionService(context);
            var categoryId = Guid.NewGuid();
            var category = new Category
            {
                Id = categoryId,
                Name = "Manga Genres"
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
            var result = context.CategoryOpinions.FirstOrDefault(co => co.Id == optionDto.Id);
            Assert.NotNull(result);
            Assert.Equal(optionDto.Key, result.Key);
            Assert.Equal(optionDto.Value, result.Value);
            Assert.Equal(optionDto.CategoryId, result.CategoryId);
        }
    }
}
