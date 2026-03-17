using Business_Logic_Layer.Services;
using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Task = System.Threading.Tasks.Task;

namespace Tests.BLL_Tests
{
    public class CategoryServiceTests : ServiceTests
    {
        [Fact]
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
    }
}
