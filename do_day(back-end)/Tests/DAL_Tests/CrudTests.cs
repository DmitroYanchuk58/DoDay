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
            // 1. Створюємо з'єднання з SQLite в пам'яті
            var connection = new Microsoft.Data.Sqlite.SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<DoDayDBContext>()
                    .UseSqlite(connection) // SQLite розуміє унікальність!
                    .Options;

                using (var context = new DoDayDBContext(options))
                {
                    // Створюємо схему таблиць
                    await context.Database.EnsureCreatedAsync();

                    var repository = new CrudRepository<User>(context);

                    var user1 = new User(Guid.NewGuid(), "user1", "same@email.com", "123", "Al", "Bibek");
                    await repository.CreateAsync(user1);

                    var user2 = new User(Guid.NewGuid(), "user2", "same@email.com", "456", "John", "Doe");

                    // Тепер SQLite реально викине помилку
                    await Assert.ThrowsAsync<DbUpdateException>(() =>
                        repository.CreateAsync(user2)
                    );
                }
            }
            finally
            {
                connection.Close();
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
        public async Task GetByIdAsync_NonExistentId_ShouldReturnNull()
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new CrudRepository<User>(context);
            var nonExistentId = Guid.NewGuid();
            // Act
            var result = await repository.GetByIdAsync(nonExistentId);
            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllEntities()
        {
            // Arrange
            using var context = GetDbContext();

            // Створюємо список тестових користувачів
            var users = new List<User>
            {
                new User(Guid.NewGuid(), "Bob", "bob@test.com", "pass123", "Bob", "Smith"),
                new User(Guid.NewGuid(), "Alice", "alice@test.com", "pass456", "Alice", "Brown"),
                new User(Guid.NewGuid(), "Charlie", "charlie@test.com", "pass789", "Charlie", "Davis")
            };

            // Додаємо їх безпосередньо в контекст для підготовки тесту
            context.Users.AddRange(users);
            await context.SaveChangesAsync();

            var repository = new CrudRepository<User>(context);

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count); // Перевіряємо кількість
            Assert.Contains(result, u => u.Username == "Bob"); // Перевіряємо наявність конкретних даних
            Assert.Contains(result, u => u.Username == "Alice");
            Assert.Contains(result, u => u.Username == "Charlie");
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
        public async Task UpdateAsync_ShouldThrowException_WhenUpdatingToExistingEmail()
        {
            // Arrange
            var connection = new Microsoft.Data.Sqlite.SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<DoDayDBContext>()
                    .UseSqlite(connection)
                    .Options;

                using (var context = new DoDayDBContext(options))
                {
                    await context.Database.EnsureCreatedAsync();
                    var repository = new CrudRepository<User>(context);

                    // 1. Створюємо двох різних користувачів
                    var user1 = new User(Guid.NewGuid(), "UserOne", "one@test.com", "123", "Al", "Bibek");
                    var user2 = new User(Guid.NewGuid(), "UserTwo", "two@test.com", "456", "John", "Doe");

                    await repository.CreateAsync(user1);
                    await repository.CreateAsync(user2);

                    // 2. Спробуємо змінити Email другого користувача на Email першого
                    user2.Email = "one@test.com";

                    // Act & Assert
                    // Очікуємо DbUpdateException через порушення унікального індексу (Unique Constraint)
                    await Assert.ThrowsAsync<DbUpdateException>(() =>
                        repository.UpdateAsync(user2)
                    );
                }
            }
            finally
            {
                connection.Close();
            }
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

        [Fact]
        public async Task DeleteAsync_ShouldNotThrowException_WhenEntityDoesNotExist()
        {
            // Arrange
            using var context = GetDbContext();
            var repository = new CrudRepository<User>(context);

            // Створюємо випадковий ID, якого точно немає в базі
            var nonExistentId = Guid.NewGuid();

            // Act
            var exception = await Record.ExceptionAsync(() => repository.DeleteAsync(nonExistentId));

            // Assert
            Assert.Null(exception);

            var count = await context.Users.CountAsync();
            Assert.Equal(0, count);
        }
    }
}