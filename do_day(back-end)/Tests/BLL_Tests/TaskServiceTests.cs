using Business_Logic_Layer.DTO;
using Business_Logic_Layer.Services;
using Data_Access_Layer.DatabaseContext;
using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using Task = System.Threading.Tasks.Task;
using TaskEntity = Data_Access_Layer.Entities.Task;

namespace Tests.BLL_Tests
{
    public class TaskServiceTests : ServiceTests
    {
        [Fact]
        public async Task GetTask_ShouldReturnTaskDTO_WhenTaskExists()
        {
            // Arrange
            using var context = GetDbContext();
            var taskId = Guid.NewGuid();
            var task = new TaskEntity(taskId, "Написати тести", DateTime.Now);
            context.Tasks.Add(task);
            await context.SaveChangesAsync();

            var taskService = new TaskService(context);

            // Act
            var result = await taskService.GetTask(taskId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Написати тести", result.Name);
            Assert.IsType<TaskDTO>(result);
        }

        [Fact]
        public async Task GetTask_ShouldThrowArgumentException_WhenIdIsEmpty()
        {
            // Arrange
            using var context = GetDbContext();
            var taskService = new TaskService(context);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                taskService.GetTask(Guid.Empty)
            );

            Assert.Equal("Id cannot be empty.", exception.Message);
        }

        [Fact]
        public async Task GetTask_ShouldThrowKeyNotFoundException_WhenTaskDoesNotExist()
        {
            // Arrange
            using var context = GetDbContext();
            var taskService = new TaskService(context);
            var fakeId = Guid.NewGuid();

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                taskService.GetTask(fakeId)
            );
        }

        [Fact]
        public async Task GetTask_ShouldMapAllFieldsFromEntityToDto()
        {
            // Arrange
            using var context = GetDbContext();
            var taskId = Guid.NewGuid();
            var task = new TaskEntity(taskId, "Title", DateTime.Now, "Description", null);
            context.Tasks.Add(task);
            await context.SaveChangesAsync();

            var taskService = new TaskService(context);

            // Act
            var result = await taskService.GetTask(taskId);

            // Assert
            Assert.Equal(task.Name, result.Name);
            Assert.Equal(task.Description, result.Description);
        }

        [Fact]
        public async Task CreateTask_ShouldSaveTaskToDatabase_WhenDataIsValid()
        {
            // Arrange
            using var context = GetDbContext();
            var taskService = new TaskService(context);
            var userId = Guid.NewGuid();
            var taskDto = new TaskDTO
            {
                Name = "Нова задача",
                Description = "Опис задачі"
            };

            // Act
            await taskService.CreateTask(taskDto, userId);

            // Assert
            var taskInDb = await context.Tasks.FirstOrDefaultAsync(t => t.Name == "Нова задача");
            Assert.NotNull(taskInDb);
            Assert.Equal(userId, taskInDb.UserId); // Перевіряємо зв'язок з юзером
            Assert.Equal(taskDto.Description, taskInDb.Description);
        }

        [Fact]
        public async Task CreateTask_ShouldThrowArgumentNullException_WhenDtoIsNull()
        {
            // Arrange
            using var context = GetDbContext();
            var taskService = new TaskService(context);
            var userId = Guid.NewGuid();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
                taskService.CreateTask(null, userId)
            );

            Assert.Contains("TaskDTO cannot be null", exception.Message);
        }


        [Fact]
        public async Task CreateTask_ShouldAddMultipleTasksForSameUser()
        {
            // Arrange
            using var context = GetDbContext();
            var taskService = new TaskService(context);
            var userId = Guid.NewGuid();

            await taskService.CreateTask(new TaskDTO { Name = "Задача 1" }, userId);

            // Act
            await taskService.CreateTask(new TaskDTO { Name = "Задача 2" }, userId);

            // Assert
            var userTasksCount = await context.Tasks.CountAsync(t => t.UserId == userId);
            Assert.Equal(2, userTasksCount);
        }

        [Fact]
        public async Task CreateTask_ShouldThrowArgumentException_WhenNameIsNullOrWhitespace()
        {
            // Arrange
            using var context = GetDbContext();
            var taskService = new TaskService(context);
            var userId = Guid.NewGuid();
            var taskDto = new TaskDTO
            {
                Name = "Some name",
                Description = "Description",
                
            };
            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                taskService.CreateTask(taskDto, userId)
            );
            Assert.Equal("Task name cannot be null or whitespace.", exception.Message);
        }

        [Fact]
        public async Task ChangeName_ShouldUpdateTitle_WhenDataIsValid()
        {
            // Arrange
            using var context = GetDbContext();
            var taskId = Guid.NewGuid();
            var originalTask = new TaskEntity(taskId, "Стара назва", DateTime.Now);
            context.Tasks.Add(originalTask);
            await context.SaveChangesAsync();

            var taskService = new TaskService(context);
            string updatedName = "Оновлена назва";

            // Act
            await taskService.ChangeName(taskId, updatedName);

            // Assert
            var taskInDb = await context.Tasks.FindAsync(taskId);
            Assert.Equal(updatedName, taskInDb.Name);
        }

        [Fact]
        public async Task ChangeName_ShouldThrowArgumentException_WhenNameIsWhitespace()
        {
            // Arrange
            using var context = GetDbContext();
            var taskService = new TaskService(context);
            var taskId = Guid.NewGuid();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                taskService.ChangeName(taskId, "   ")
            );

            Assert.Equal("New name cannot be null or whitespace.", exception.Message);
        }

        [Fact]
        public async Task ChangeName_ShouldThrowArgumentException_WhenIdIsEmpty()
        {
            // Arrange
            using var context = GetDbContext();
            var taskService = new TaskService(context);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() =>
                taskService.ChangeName(Guid.Empty, "Нова назва")
            );
        }

        [Fact]
        public async Task ChangeName_ShouldThrowKeyNotFoundException_WhenTaskDoesNotExist()
        {
            // Arrange
            using var context = GetDbContext();
            var taskService = new TaskService(context);
            var fakeId = Guid.NewGuid();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                taskService.ChangeName(fakeId, "Будь-яка назва")
            );

            Assert.Contains(fakeId.ToString(), exception.Message);
        }

        [Fact]
        public async Task ChangeDescription_ShouldUpdateDescription_WhenDataIsValid()
        {
            // Arrange
            using var context = GetDbContext();
            var taskId = Guid.NewGuid();
            var task = new Data_Access_Layer.Entities.Task(taskId, "Назва", DateTime.Now);
            context.Tasks.Add(task);
            await context.SaveChangesAsync();

            var taskService = new TaskService(context);
            string newDesc = "Це абсолютно новий опис завдання";

            // Act
            await taskService.ChangeDescription(taskId, newDesc);

            // Assert
            var taskInDb = await context.Tasks.FindAsync(taskId);
            Assert.Equal(newDesc, taskInDb.Description);
        }

        [Fact]
        public async Task ChangeDescription_ShouldThrowArgumentException_WhenDescriptionIsNullOrWhitespace()
        {
            // Arrange
            using var context = GetDbContext();
            var taskService = new TaskService(context);
            var taskId = Guid.NewGuid();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                taskService.ChangeDescription(taskId, "   ")
            );

            Assert.Equal("New description cannot be null or whitespace.", exception.Message);
        }

        [Fact]
        public async Task ChangeDescription_ShouldThrowKeyNotFoundException_WhenTaskDoesNotExist()
        {
            // Arrange
            using var context = GetDbContext();
            var taskService = new TaskService(context);
            var fakeId = Guid.NewGuid();

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                taskService.ChangeDescription(fakeId, "Новий опис")
            );
        }

        [Fact]
        public async Task ChangeDescription_ShouldThrowArgumentException_WhenIdIsEmpty()
        {
            // Arrange
            using var context = GetDbContext();
            var taskService = new TaskService(context);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() =>
                taskService.ChangeDescription(Guid.Empty, "Опис")
            );
        }

        [Fact]
        public async Task ChangeImage_ShouldUpdateImage_WhenDataIsValid()
        {
            // Arrange
            using var context = GetDbContext();
            var taskId = Guid.NewGuid();
            var task = new Data_Access_Layer.Entities.Task(taskId, "Task with Image", DateTime.Now);
            context.Tasks.Add(task);
            await context.SaveChangesAsync();

            var taskService = new TaskService(context);
            byte[] newImageData = { 0x01, 0x02, 0x03, 0x04 }; // імітація файлу

            // Act
            await taskService.ChangeImage(taskId, newImageData);

            // Assert
            var taskInDb = await context.Tasks.FindAsync(taskId);
            Assert.Equal(newImageData, taskInDb.Image);
        }

        [Fact]
        public async Task ChangeImage_ShouldThrowArgumentException_WhenImageIsNullOrEmpty()
        {
            // Arrange
            using var context = GetDbContext();
            var taskService = new TaskService(context);
            var taskId = Guid.NewGuid();

            // Act & Assert (для null)
            await Assert.ThrowsAsync<ArgumentException>(() =>
                taskService.ChangeImage(taskId, null)
            );

            // Act & Assert (для порожнього масиву)
            await Assert.ThrowsAsync<ArgumentException>(() =>
                taskService.ChangeImage(taskId, Array.Empty<byte>())
            );
        }

        [Fact]
        public async Task ChangeImage_ShouldThrowKeyNotFoundException_WhenTaskDoesNotExist()
        {
            // Arrange
            using var context = GetDbContext();
            var taskService = new TaskService(context);
            var fakeId = Guid.NewGuid();
            byte[] someData = { 0x11 };

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                taskService.ChangeImage(fakeId, someData)
            );
        }

        [Fact]
        public async Task DeleteTask_ShouldRemoveTaskFromDatabase_WhenTaskExists()
        {
            // Arrange
            using var context = GetDbContext();
            var taskId = Guid.NewGuid();
            var task = new TaskEntity(taskId, "Task to delete", DateTime.Now);
            context.Tasks.Add(task);
            await context.SaveChangesAsync();

            var taskService = new TaskService(context);

            // Act
            await taskService.DeleteTask(taskId);

            // Assert
            var taskInDb = await context.Tasks.FindAsync(taskId);
            Assert.Null(taskInDb);
        }

        [Fact]
        public async Task DeleteTask_ShouldThrowArgumentException_WhenIdIsEmpty()
        {
            // Arrange
            using var context = GetDbContext();
            var taskService = new TaskService(context);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                taskService.DeleteTask(Guid.Empty)
            );

            Assert.Equal("Id cannot be empty.", exception.Message);
        }

        [Fact]
        public async Task DeleteTask_ShouldNotThrowException_WhenTaskDoesNotExist()
        {
            // Arrange
            using var context = GetDbContext();
            var taskService = new TaskService(context);
            var fakeId = Guid.NewGuid();

            // Act
            var exception = await Record.ExceptionAsync(() =>
                taskService.DeleteTask(fakeId)
            );

            // Assert
            Assert.Null(exception);
        }

        [Fact]
        public async Task UpdateTask_ShouldUpdateTask_WhenInfoIsCorrect()
        {

            // Arrange
            using var context = GetDbContext();
            var taskService = new TaskService(context);
            var task = new TaskDTO
            {
                Id = Guid.NewGuid(),
                Name = "Original Task",
                Description = "Original Description"
            };

            // Act
            await taskService.CreateTask(task, Guid.NewGuid());
            context.ChangeTracker.Clear();
            task.Name = "Updated Task";
            task.Description = "Updated Description";

            await taskService.UpdateTask(task);
            context.ChangeTracker.Clear();
            var updatedTask = await taskService.GetTask(task.Id.Value);
            context.ChangeTracker.Clear();

            // Assert

            Assert.Equal("Updated Task", updatedTask.Name);
            Assert.Equal("Updated Description", updatedTask.Description);
        }

        [Fact]
        public async Task UpdateTask_ShouldThrowArgumentNullException_WhenDtoIsNull()
        {
            // Arrange
            using var context = GetDbContext();
            var taskService = new TaskService(context);
            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() =>
                taskService.UpdateTask(null!)
            );
        }

        [Fact]
        public async Task UpdateTask_ShouldThrowArgumentException_WhenIdIsEmpty()
        {
            // Arrange
            using var context = GetDbContext();
            var taskService = new TaskService(context);
            var taskDto = new TaskDTO
            {
                Id = Guid.Empty,
                Name = "Task with Empty Id"
            };
            // Act & Assert 
            var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
                taskService.UpdateTask(taskDto)
            );
        }

        [Fact]
        public async Task UpdateTask_ShouldThrowKeyNotFoundException_WhenTaskDoesNotExist()
        {
            // Arrange
            using var context = GetDbContext();
            var taskService = new TaskService(context);
            var taskDto = new TaskDTO
            {
                Id = Guid.NewGuid(),
                Name = "Non-existent Task"
            };
            // Act & Assert
            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
                taskService.UpdateTask(taskDto)
            );
            Assert.Contains(taskDto.Id.ToString(), exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task UpdateManga_ShouldThrowException_WhenTitleIsInvalid(string invalidTitle)
        {
            // Arrange
            using var context = GetDbContext();
            var service = new TaskService(context);
            var userId = Guid.NewGuid();
            var taskDto = new TaskDTO
            {
                Id = Guid.NewGuid(),
                Name = "Task to Update",
                Description = "Original Description"
            };
            // Act
            await service.CreateTask(taskDto, userId);
            taskDto.Name = invalidTitle;

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => service.UpdateTask(taskDto));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task UpdateManga_ShouldThrowException_WhenDescriptionIsInvalid(string invalidDescription)
        {
            // Arrange
            using var context = GetDbContext();
            var service = new TaskService(context);
            var userId = Guid.NewGuid();
            var taskDto = new TaskDTO
            {
                Id = Guid.NewGuid(),
                Name = "Task to Update",
                Description = "Original Description"
            };
            // Act
            await service.CreateTask(taskDto, userId);
            taskDto.Description = invalidDescription;

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => service.UpdateTask(taskDto));
        }

        [Fact]
        public async Task UpdateReadItem_ShouldThrowException_WhenNameIsToLong()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new TaskService(context);
            var userId = Guid.NewGuid();
            var taskDto = new TaskDTO
            {
                Id = Guid.NewGuid(),
                Name = "Task to Update",
                Description = "Original Description"
            };
            // Act
            await service.CreateTask(taskDto, userId);
            var longName = new string('A', 501);
            taskDto.Name = longName;

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => service.UpdateTask(taskDto));
        }

        [Fact]
        public async Task UpdateReadItem_ShouldThrowException_WhenDescriptionIsToLong()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new TaskService(context);
            var userId = Guid.NewGuid();
            var taskDto = new TaskDTO
            {
                Id = Guid.NewGuid(),
                Name = "Task to Update",
                Description = "Original Description"
            };
            // Act
            await service.CreateTask(taskDto, userId);
            var longDescription = new string('A', 3001);
            taskDto.Description = longDescription;
            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => service.UpdateTask(taskDto));
        }

        [Fact]
        public async Task UpdateReadItem_ShouldThrowException_WhenDescriptionIsTooShort()
        {
            // Arrange
            using var context = GetDbContext();
            var service = new TaskService(context);
            var userId = Guid.NewGuid();
            var taskDto = new TaskDTO
            {
                Id = Guid.NewGuid(),
                Name = "Task to Update",
                Description = "Original Description"
            };
            // Act
            await service.CreateTask(taskDto, userId);
            taskDto.Description = "12"; 
            // Assert
            await Assert.ThrowsAsync<ArgumentException>(() => service.UpdateTask(taskDto));
        }
    }
}
