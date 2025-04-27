using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> CreateTask(CreateTaskDto taskDto)
        {
            var task = taskDto.ToTaskFromCreateDto();
            await _context.TaskList.AddAsync(task);
            await _context.SaveChangesAsync();
            return new OkObjectResult(task.ToTaskDto());
        }

        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var task = await FindTask(id);
            if (task == null) {
                return new NotFoundObjectResult(new ResponseModel {
                    Status = "Error",
                    Message = $"Task with id={id} wasn't found"
                });
            }
            _context.TaskList.Remove(task);
            await _context.SaveChangesAsync();
            return new OkObjectResult(task.Id);
        }

        public async Task<IActionResult> EditTask(UpdateTaskDto taskDto, Guid id)
        {
            var task = await FindTask(id);
            if (task == null) {
                return new NotFoundObjectResult(new ResponseModel {
                    Status = "Error",
                    Message = $"Task with id={id} wasn't found"
                });
            }
            task.Name = taskDto.Name;
            task.Description = taskDto.Description;
            task.Priority = taskDto.Priority ?? Priority.Medium;
            task.Deadline = taskDto.Deadline;
            task.UpdatedTime = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return new OkObjectResult(task.ToTaskDto()); 
        }

        public async Task<TaskElement?> FindTask(Guid id)
        {
            var task = await _context.TaskList.FirstOrDefaultAsync(t => t.Id == id);
            return task;
        }

        public async Task<IActionResult> GetAllTasks() //add query later
        {
            var tasks = await _context.TaskList
                .Select(t => t.ToTaskDto())
                .ToListAsync();
            return new OkObjectResult(tasks);
        }

        public async Task<IActionResult> GetFullTask(Guid id)
        {
            var task = await FindTask(id);
            if (task == null) {
                return new NotFoundObjectResult(new ResponseModel {
                    Status = "Error",
                    Message = $"Task with id={id} wasn't found"
                });
            }
            var fullTask = task.ToFullTaskDto();
            return new OkObjectResult(fullTask);
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

        public async Task<IActionResult> ToggleTask(Guid id)
        {
            var task = await FindTask(id);
            if (task == null) {
                return new NotFoundObjectResult(new ResponseModel {
                    Status = "Error",
                    Message = $"Task with id={id} wasn't found"
                });
            }
            task.IsChecked = !task.IsChecked;
            await _context.SaveChangesAsync();
            return new OkObjectResult(task.ToTaskDto());
        }
    }
}