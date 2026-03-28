using Business_Logic_Layer.DTO;
using Business_Logic_Layer.Services.Interfaces;
using Business_Logic_Layer.Validators;
using Data_Access_Layer.DatabaseContext;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Task = System.Threading.Tasks.Task;
using TaskEntity = Data_Access_Layer.Entities.Task;

namespace Business_Logic_Layer.Services
{
    public class TaskService : ITaskService
    {
        CrudRepository<TaskEntity> _taskRepository;

        public TaskService(DoDayDBContext context)
        {
            _taskRepository = new CrudRepository<TaskEntity>(context);
        }

        public async Task<TaskDTO> GetTask(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id cannot be empty.");
            }

            var taskEntity = await _taskRepository.GetByIdAsync(id);

            if (taskEntity == null)
            {
                throw new KeyNotFoundException($"Task with id {id} was not found.");
            }

            var taskDto = new TaskDTO(taskEntity);
            return taskDto;
        }

        public async Task<List<TaskDTO>> GetTasksByUserId(Guid idUser)
        {
            var taskEntities = (await _taskRepository.GetAllAsync()).Where(e => e.UserId == idUser);

            var taskDtos = new List<TaskDTO>();
            foreach (var taskEntity in taskEntities)
            {
                var taskDto = new TaskDTO(taskEntity);
                taskDtos.Add(taskDto);
            }
            return taskDtos;
        }

        public async Task CreateTask(TaskDTO taskDto, Guid idUser)
        {
            if (taskDto == null)
            {
                throw new ArgumentNullException(nameof(taskDto), "TaskDTO cannot be null.");
            }
            var validator = new TaskDTOValidator();
            var result = validator.Validate(taskDto);
            if (!result.IsValid)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                throw new ArgumentException(errors);
            }
            var taskEntity = taskDto.ToEntity(idUser);
            await _taskRepository.CreateAsync(taskEntity);
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
            var taskEntity = await _taskRepository.GetByIdAsync(id);
            if (taskEntity == null)
            {
                throw new KeyNotFoundException($"Task with id {id} was not found.");
            }
            var taskDto = new TaskDTO(taskEntity);
            taskDto.Name = newName;
            taskEntity.Name = taskDto.Name;
            await _taskRepository.UpdateAsync(taskEntity);
        }

        public async Task ChangeDescription(Guid id, string newDescription)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id cannot be empty.");
            }
            if (string.IsNullOrWhiteSpace(newDescription))
            {
                throw new ArgumentException("New description cannot be null or whitespace.");
            }
            var taskEntity = await _taskRepository.GetByIdAsync(id);
            if (taskEntity == null)
            {
                throw new KeyNotFoundException($"Task with id {id} was not found.");
            }
            var taskDto = new TaskDTO(taskEntity);
            taskDto.Description = newDescription;
            taskEntity.Description = taskDto.Description;
            await _taskRepository.UpdateAsync(taskEntity);
        }

        public async Task ChangeImage(Guid id, byte[] newImage)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id cannot be empty.");
            }
            if (newImage == null || newImage.Length == 0)
            {
                throw new ArgumentException("New image cannot be null or empty.");
            }
            var taskEntity = await _taskRepository.GetByIdAsync(id);
            if (taskEntity == null)
            {
                throw new KeyNotFoundException($"Task with id {id} was not found.");
            }
            var taskDto = new TaskDTO(taskEntity);
            taskDto.Image = newImage;
            taskEntity.Image = taskDto.Image;
            await _taskRepository.UpdateAsync(taskEntity);
        }

        public async Task DeleteTask(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id cannot be empty.");
            }
            await _taskRepository.DeleteAsync(id);
        }
        
    }
}
