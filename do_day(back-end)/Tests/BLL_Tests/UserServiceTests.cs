using Business_Logic_Layer.DTO;
using Business_Logic_Layer.Services;
using Business_Logic_Layer.Services.Interfaces;
using Data_Access_Layer.DatabaseContext;
using Data_Access_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace Tests.BLL_Tests
{
    public class UserServiceTests : ServiceTests
    {
        [Fact]
        public async Task Login_ShouldReturnTrue_WhenCredentialsAreCorrect()
        {
            // Arrange
            using var context = GetDbContext();
            var user = new User(Guid.NewGuid(), "tester", "test@test.com", "correct_pass", "Al", "Bibek");
            context.Users.Add(user);
            await context.SaveChangesAsync();

            IAuthorizationService userService = new UserService(context);

            // Act
            var result = await userService.Login("test@test.com", "correct_pass");

            // Assert
            Assert.True(result.Item1);
        }

        [Fact]
        public async Task Login_ShouldReturnFalse_WhenPasswordIsIncorrect()
        {
            // Arrange
            using var context = GetDbContext();
            var user = new User(Guid.NewGuid(), "tester", "test@test.com", "secret", "Al", "Bibek");
            context.Users.Add(user);
            await context.SaveChangesAsync();

            IAuthorizationService userService = new UserService(context);

            // Act
            var result = await userService.Login("test@test.com", "wrong_password");

            // Assert
            Assert.False(result.Item1);
        }

        [Fact]
        public async Task Login_ShouldThrowException_WhenUserDoesNotExist()
        {
            // Arrange
            using var context = GetDbContext();
            // База порожня
            IAuthorizationService userService = new UserService(context);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() =>
                userService.Login("not_found@test.com", "any_pass")
            );
        }

        [Fact]
        public async Task Login_ShouldReturnTrue_WhenEmailCaseDiffers()
        {
            // Arrange
            using var context = GetDbContext();
            var user = new User(Guid.NewGuid(), "tester", "user@test.com", "123", "Al", "Bibek");
            context.Users.Add(user);
            await context.SaveChangesAsync();

            IAuthorizationService userService = new UserService(context);

            // Act
            // Передаємо Email великими літерами
            var result = await userService.Login("USER@TEST.COM", "123");

            // Assert
            Assert.True(result.Item1);
        }

        [Fact]
        public async Task Register_ShouldCreateUser_WhenDataIsValid()
        {
            // Arrange
            using var context = GetDbContext();
            IAuthorizationService userService = new UserService(context);
            string email = "newuser@test.com";

            var user = new UserDTO
            {
                Username = "CoolUser",
                Password = "password123",
                Email = email,
                FirstName = "Ivan",
                LastName = "Ivanov"
            };

            // Act
            await userService.Register(user);

            // Assert
            var savedUser = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
            Assert.NotNull(savedUser);
            Assert.Equal("CoolUser", savedUser.Username);
            Assert.Equal("Ivan", savedUser.FirstName);
        }

        [Fact]
        public async Task Register_ShouldThrowException_WhenEmailAlreadyExists()
        {
            // Arrange
            var connection = new Microsoft.Data.Sqlite.SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<DoDayDBContext>().UseSqlite(connection).Options;

            using var context = new DoDayDBContext(options);
            await context.Database.EnsureCreatedAsync();

            IAuthorizationService userService = new UserService(context);

            var user = new UserDTO
            {
                Username = "user1",
                Password = "pass",
                Email = "same@test.com",
                FirstName = "A",
                LastName = "B"
            };
            await userService.Register(user);
            user.Username = "user2";
            user.FirstName = "C";
            user.LastName = "D";

            // Act & Assert
            await Assert.ThrowsAsync<DbUpdateException>(() =>
                userService.Register(user)
            );
        }

        [Fact]
        public async Task Register_ShouldAssignNewGuid_ToNewUser()
        {
            // Arrange
            using var context = GetDbContext();
            IAuthorizationService userService = new UserService(context);
            var user = new UserDTO
            {
                Username = "user",
                Password = "pass",
                Email = "id-test@test.com",
                FirstName = "A",
                LastName = "B"
            };

            // Act
            await userService.Register(user);

            // Assert
            var userDB = await context.Users.FirstAsync(u => u.Email == "id-test@test.com");
            Assert.NotEqual(Guid.Empty, userDB.Id);
        }

        [Fact]
        public async Task Register_ShouldThrowDbUpdateException_WhenUsernameTooLongForDatabase()
        {
            // Arrange
            var connection = new Microsoft.Data.Sqlite.SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<DoDayDBContext>()
                .UseSqlite(connection)
                .Options;

            using (var context = new DoDayDBContext(options))
            {
                var userService = new UserService(context);

                string longUsername = new string('A', 200);

                var user = new UserDTO
                {
                    Username = longUsername,
                    Password = "pass123",
                    Email = "test@test.com",
                    FirstName = "Ivan",
                    LastName = "Ivanov"
                };

                // Act & Assert
                await Assert.ThrowsAsync<DbUpdateException>(async () =>
                {
                    await userService.Register(user);
                });
            }
        }

        [Fact]
        public async Task ChangePassword_ShouldUpdatePassword_WhenOldPasswordIsCorrect()
        {
            // Arrange
            using var context = GetDbContext();
            var userId = Guid.NewGuid();
            var user = new User(userId, "user", "test@test.com", "old_secret", "Al", "Bibek");
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var userService = new UserService(context);
            string newPass = "new_secure_pass";

            // Act
            await userService.ChangePassword(userId, "old_secret", newPass);

            // Assert
            var updatedUser = await context.Users.FindAsync(userId);
            Assert.Equal(newPass, updatedUser.Password);
        }

        [Fact]
        public async Task ChangePassword_ShouldThrowException_WhenOldPasswordIsIncorrect()
        {
            // Arrange
            using var context = GetDbContext();
            var userId = Guid.NewGuid();
            var user = new User(userId, "user", "test@test.com", "real_password", "Al", "Bibek");
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var userService = new UserService(context);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() =>
                userService.ChangePassword(userId, "wrong_password", "any_new_pass")
            );

            Assert.Equal("Old password is incorrect", exception.Message);
        }

        [Fact]
        public async Task ChangePassword_ShouldThrowException_WhenUserDoesNotExist()
        {
            // Arrange
            using var context = GetDbContext();
            var userService = new UserService(context);
            var fakeId = Guid.NewGuid();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() =>
                userService.ChangePassword(fakeId, "pass", "new_pass")
            );

            Assert.Equal("User not found", exception.Message);
        }

        [Fact]
        public async Task ChangeNickname_ShouldUpdateUsername_WhenUserExists()
        {
            // Arrange
            using var context = GetDbContext();
            var userId = Guid.NewGuid();
            var user = new User(userId, "OldName", "test@test.com", "pass", "Al", "Bibek");
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var userService = new UserService(context);
            string newNickname = "NewCoolName";

            // Act
            await userService.ChangeNickname(userId, newNickname);

            // Assert
            var updatedUser = await context.Users.FindAsync(userId);
            Assert.NotNull(updatedUser);
            Assert.Equal(newNickname, updatedUser.Username);
        }

        [Fact]
        public async Task ChangeNickname_ShouldThrowException_WhenUserDoesNotExist()
        {
            // Arrange
            using var context = GetDbContext();
            var userService = new UserService(context);
            var fakeId = Guid.NewGuid();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() =>
                userService.ChangeNickname(fakeId, "NewName")
            );

            Assert.Equal("User not found", exception.Message);
        }

        [Fact]
        public async Task ChangeNickname_ShouldNotThrowTrackingError()
        {
            // Arrange
            using var context = GetDbContext();
            var userId = Guid.NewGuid();
            context.Users.Add(new User(userId, "Original", "email@test.com", "123", "A", "B"));
            await context.SaveChangesAsync();

            var userService = new UserService(context);

            // Act
            var exception = await Record.ExceptionAsync(() =>
                userService.ChangeNickname(userId, "UpdatedName")
            );

            // Assert
            Assert.Null(exception); 
        }

        [Fact]
        public async Task ChangeNickname_ShouldSaveNewNicknameToDatabase()
        {
            // Arrange
            using var context = GetDbContext();
            var userId = Guid.NewGuid();
            context.Users.Add(new User(userId, "OldName", "test@test.com", "pass", "Ivan", "Ivanov"));
            await context.SaveChangesAsync();

            var userService = new UserService(context);
            string newNickname = "NewCoolName";

            // Act
            await userService.ChangeNickname(userId, newNickname);

            // Assert
            var userInDb = await context.Users.FindAsync(userId);
            Assert.Equal(newNickname, userInDb.Username);
        }

        [Fact]
        public async Task ChangeNickname_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            using var context = GetDbContext();
            var userService = new UserService(context);
            var fakeId = Guid.NewGuid();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() =>
                userService.ChangeNickname(fakeId, "SomeName")
            );

            Assert.Equal("User not found", exception.Message);
        }

        [Fact]
        public async Task ChangeNickname_ShouldNotThrowTrackingConflictException()
        {
            // Arrange
            using var context = GetDbContext();
            var userId = Guid.NewGuid();
            context.Users.Add(new User(userId, "Original", "email@test.com", "123", "A", "B"));
            await context.SaveChangesAsync();

            var userService = new UserService(context);

            // Act
            var exception = await Record.ExceptionAsync(() =>
                userService.ChangeNickname(userId, "Updated")
            );

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task UploadProfilePicture_ShouldSaveBytes_WhenUserExists()
        {
            // Arrange
            using var context = GetDbContext();
            var userId = Guid.NewGuid();
            var user = new User(userId, "tester", "test@test.com", "123", "Ivan", "Ivanov");
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var userService = new UserService(context);
            byte[] mockPicture = { 0x01, 0x02, 0x03, 0x04 };

            // Act
            await userService.UploadProfilePicture(userId, mockPicture);

            // Assert
            var updatedUser = await context.Users.FindAsync(userId);
            Assert.Equal(mockPicture, updatedUser.ProfilePicture);
        }

        [Fact]
        public async Task UploadProfilePicture_ShouldThrowException_WhenUserDoesNotExist()
        {
            // Arrange
            using var context = GetDbContext();
            var userService = new UserService(context);
            var fakeId = Guid.NewGuid();
            byte[] mockPicture = { 0x01 };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() =>
                userService.UploadProfilePicture(fakeId, mockPicture)
            );

            Assert.Equal("User not found", exception.Message);
        }

        [Fact]
        public async Task UploadProfilePicture_ShouldOverwriteExistingPicture()
        {
            // Arrange
            using var context = GetDbContext();
            var userId = Guid.NewGuid();
            byte[] oldPicture = { 0xAA, 0xBB };
            byte[] newPicture = { 0xCC, 0xDD };

            var user = new User(userId, "tester", "test@test.com", "123", "A", "B");
            user.ProfilePicture = oldPicture;
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var userService = new UserService(context);

            // Act
            await userService.UploadProfilePicture(userId, newPicture);

            // Assert
            var userInDb = await context.Users.FindAsync(userId);
            Assert.Equal(newPicture, userInDb.ProfilePicture);
            Assert.NotEqual(oldPicture, userInDb.ProfilePicture);
        }

        [Fact]
        public async Task UploadProfilePicture_ShouldAllowEmptyArray_WhenRemovingPicture()
        {
            // Arrange
            using var context = GetDbContext();
            var userId = Guid.NewGuid();
            var user = new User(userId, "tester", "test@test.com", "123", "A", "B");
            user.ProfilePicture = new byte[] { 0x01 };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var userService = new UserService(context);
            byte[] emptyPicture = Array.Empty<byte>();

            // Act
            await userService.UploadProfilePicture(userId, emptyPicture);

            // Assert
            var userInDb = await context.Users.FindAsync(userId);
            Assert.Empty(userInDb.ProfilePicture);
        }

        [Fact]
        public async Task UploadProfilePicture_ShouldNotThrowTrackingConflict()
        {
            // Arrange
            using var context = GetDbContext();
            var userId = Guid.NewGuid();
            context.Users.Add(new User(userId, "tester", "email@t.com", "123", "A", "B"));
            await context.SaveChangesAsync();

            var userService = new UserService(context);

            // Act
            var exception = await Record.ExceptionAsync(() =>
                userService.UploadProfilePicture(userId, new byte[] { 0x01 })
            );

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task ChangeNumber_ShouldUpdatePhoneNumber_WhenUserExists()
        {
            // Arrange
            using var context = GetDbContext();
            var userId = Guid.NewGuid();
            var user = new User(userId, "tester", "test@test.com", "pass", "A", "B")
            {
                Number = "123456789"
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var userService = new UserService(context);
            string newNumber = "987654321";

            // Act
            await userService.ChangeNumber(userId, newNumber);

            // Assert
            var updatedUser = await context.Users.FindAsync(userId);
            Assert.NotNull(updatedUser);
            Assert.Equal(newNumber, updatedUser.Number);
        }

        [Fact]
        public async Task ChangeNumber_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            using var context = GetDbContext();
            var userService = new UserService(context);
            var fakeId = Guid.NewGuid();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() =>
                userService.ChangeNumber(fakeId, "000000000")
            );

            Assert.Equal("User not found", exception.Message);
        }

        [Fact]
        public async Task ChangeNumber_ShouldOnlyUpdateNumberField()
        {
            // Arrange
            using var context = GetDbContext();
            var userId = Guid.NewGuid();
            string originalEmail = "keep-me@test.com";
            var user = new User(userId, "tester", originalEmail, "pass", "A", "B");
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var userService = new UserService(context);

            // Act
            await userService.ChangeNumber(userId, "555-555");

            // Assert
            var userInDb = await context.Users.FindAsync(userId);
            Assert.Equal("555-555", userInDb.Number);
            Assert.Equal(originalEmail, userInDb.Email); 
        }

        [Fact]
        public async Task DeleteUser_ShouldRemoveUser_WhenUserExists()
        {
            // Arrange
            using var context = GetDbContext();
            var userId = Guid.NewGuid();
            var user = new User(userId, "tobedeleted", "delete@test.com", "123", "A", "B");
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var userService = new UserService(context);

            // Act
            await userService.DeleteUser(userId);

            // Assert
            var deletedUser = await context.Users.FindAsync(userId);
            Assert.Null(deletedUser); 
        }

        [Fact]
        public async Task DeleteUser_ShouldNotThrowException_WhenUserDoesNotExist()
        {
            // Arrange
            using var context = GetDbContext();
            var userService = new UserService(context);
            var fakeId = Guid.NewGuid();

            // Act
            var exception = await Record.ExceptionAsync(() => userService.DeleteUser(fakeId));

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task DeleteUser_ShouldMakeUserUngettableAfterDeletion()
        {
            // Arrange
            using var context = GetDbContext();
            var userId = Guid.NewGuid();
            context.Users.Add(new User(userId, "test", "test@t.com", "123", "A", "B"));
            await context.SaveChangesAsync();

            var userService = new UserService(context);

            // Act
            await userService.DeleteUser(userId);

            // Assert & Act again
            var exception = await Assert.ThrowsAsync<Exception>(() =>
                userService.GetUserById(userId)
            );
            Assert.Equal("User not found", exception.Message);
        }

        [Fact]
        public async Task GetUserById_ShouldReturnUserDTO_WhenUserExists()
        {
            // Arrange
            using var context = GetDbContext();
            var userId = Guid.NewGuid();
            var expectedUsername = "TargetUser";
            context.Users.Add(new User(userId, expectedUsername, "test@test.com", "pass", "A", "B"));
            await context.SaveChangesAsync();

            var userService = new UserService(context);

            // Act
            var result = await userService.GetUserById(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<UserDTO>(result);
            Assert.Equal(expectedUsername, result.Username);
        }

        [Fact]
        public async Task GetUserById_ShouldThrowException_WhenUserDoesNotExist()
        {
            // Arrange
            using var context = GetDbContext();
            var userService = new UserService(context);
            var fakeId = Guid.NewGuid();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() =>
                userService.GetUserById(fakeId)
            );

            Assert.Equal("User not found", exception.Message);
        }

        [Fact]
        public async Task GetUserById_ShouldMapAllFieldsCorrectly()
        {
            // Arrange
            using var context = GetDbContext();
            var userId = Guid.NewGuid();
            var user = new User(userId, "Nick", "nick@t.com", "123", "Ivan", "Ivanov")
            {
                Position = "Developer",
                Number = "555"
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var userService = new UserService(context);

            // Act
            var dto = await userService.GetUserById(userId);

            // Assert
            Assert.Equal(user.Email, dto.Email);
            Assert.Equal(user.FirstName, dto.FirstName);
            Assert.Equal(user.Position, dto.Position);
        }
    }
}
