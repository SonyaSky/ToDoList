using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Service
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDBContext _context;
        public TaskService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<TaskElement?> CreateTask(CreateTaskDto taskDto)
        {
            var task = taskDto.ToTaskFromCreateDto();
            await _context.TaskList.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteTask(Guid id)
        {
            var task = await FindTask(id);
            if (task == null) return false;
            _context.TaskList.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TaskElement?> EditTask(UpdateTaskDto taskDto, Guid id)
        {
            var task = await FindTask(id);
            if (task == null) return null;
            task.Name = taskDto.Name;
            task.Description = taskDto.Description;
            task.Priority = taskDto.Priority ?? Priority.Medium;
            task.Deadline = taskDto.Deadline;
            task.UpdatedTime = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return task; //add status
        }

        public async Task<TaskElement?> FindTask(Guid id)
        {
            var task = await _context.TaskList.FirstOrDefaultAsync(t => t.Id == id);
            return task;
        }

        public async Task<List<TaskDto>> GetAllTasks() //add query later
        {
            var tasks = await _context.TaskList
                .Select(t => t.ToTaskDto())
                .ToListAsync();
            return tasks;
        }

        public Status GetStatus(TaskElement task)
        {
            if (DateTime.UtcNow < task.Deadline) {
                if (task.IsChecked) {
                    return Status.Completed;
                } else {
                    return Status.Active;
                }
            } else {
                if (task.IsChecked) {
                    return Status.Late;
                } else {
                    return Status.Overdue;
                }
            }
        }

        public async Task<TaskElement?> ToggleTask(Guid id)
        {
            var task = await FindTask(id);
            if (task == null) return null;
            task.IsChecked = !task.IsChecked;
            await _context.SaveChangesAsync();
            return task;
        }
    }
}