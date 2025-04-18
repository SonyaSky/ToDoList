using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Models;

namespace api.Service
{
    public interface ITaskService
    {
        Task<TaskElement?> CreateTask(CreateTaskDto taskDto);
        Task<TaskElement?> ToggleTask(Guid id);
        Task<TaskElement?> EditTask(UpdateTaskDto taskDto, Guid id);
        Task<List<TaskDto>> GetAllTasks();
        Task<bool> DeleteTask(Guid id);
        Status GetStatus(TaskElement task);
        Task<TaskElement?> FindTask(Guid id);
    }
}