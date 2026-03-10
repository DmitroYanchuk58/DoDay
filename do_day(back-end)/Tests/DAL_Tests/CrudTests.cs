using Microsoft.EntityFrameworkCore;
using Data_Access_Layer.DatabaseContext;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Repositories;
using Task = System.Threading.Tasks.Task;
using Xunit;

namespace DoDay.Tests
{
    public class CrudRepositoryTests
    {
        // Метод для створення "чистого" контексту в пам'яті для кожного тесту
        private DoDayDBContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<DoDayDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Унікальне ім'я для ізоляції
                .Options;

            return new DoDayDBContext(options);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddEntityToDatabase()
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new CrudRepository<User>(context);
            var user = new User(Guid.NewGuid(), "JohnDoe", "test@test.com","123", "al", "albibek");

            // Act
            await repository.CreateAsync(user);

            // Assert
            var savedUser = await context.Users.FindAsync(user.Id);
            Assert.NotNull(savedUser);
            Assert.Equal("JohnDoe", savedUser.Username);
        }

        [Fact]
        public async Task CreateAsync_Null_ShouldThrowException()
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new CrudRepository<User>(context);
            var user = new User(Guid.NewGuid(), null!, "test@test.com", "123", "al", "albibek");

            // Act
            Func<Task> act = () => repository.CreateAsync(user);

            // Assert
            var exception = await Assert.ThrowsAsync<DbUpdateException>(act);
        }

        [Fact]
        public async Task CreateAsync_ShouldThrowException_WhenEmailIsNotUnique()
        {
            // Arrange
            using (var context = GetDbContext())
            {
                await context.Database.EnsureCreatedAsync(); // Створюємо таблиці згідно з вашим Fluent API

                var repository = new CrudRepository<User>(context);

                var user1 = new User(Guid.NewGuid(), "user1", "same@email.com", "123", "Al", "Bibek");
                await repository.CreateAsync(user1);

                // Act & Assert
                var user2 = new User(Guid.NewGuid(), "user2", "same@email.com", "456", "John", "Doe");

                // Очікуємо DbUpdateException від Entity Framework
                await Assert.ThrowsAsync<DbUpdateException>(() =>
                    repository.CreateAsync(user2)
                );
            }
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectEntity()
        {
            // Arrange
            using var context = GetDbContext();
            var userId = Guid.NewGuid();
            context.Users.Add(new User (userId, "Alice", "alice@test.com", "lklklk", "Alice", "Kennedy"));
            await context.SaveChangesAsync();

            var repository = new CrudRepository<User>(context);

            // Act
            var result = await repository.GetByIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Alice", result.Username);
        }

        [Fact]
        public async Task UpdateAsync_ShouldModifyExistingData()
        {
            // Arrange
            using var context = GetDbContext();
            var user = new User (Guid.NewGuid(), "old@test.com", "OldName", "ghtuye8wu", "Red", "From Prison");
            context.Users.Add(user);
            await context.SaveChangesAsync();
            context.Entry(user).State = EntityState.Detached; // Від'єднуємо для імітації нового запиту

            var repository = new CrudRepository<User>(context);
            user.Username = "NewName";

            // Act
            await repository.UpdateAsync(user);

            // Assert
            var updatedUser = await context.Users.FindAsync(user.Id);
            Assert.Equal("NewName", updatedUser.Username);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveEntity()
        {
            // Arrange
            using var context = GetDbContext();
            var userId = Guid.NewGuid();
            var user = new User ( userId, "del@test.com","ToDelete", "fggweg", "John", "Doe");
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var repository = new CrudRepository<User>(context);

            // Act
            await repository.DeleteAsync(userId);

            // Assert
            var result = await context.Users.FindAsync(userId);
            Assert.Null(result);
        }
    }
}