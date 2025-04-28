using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            var (newName, priority) = await FindPriority(taskDto.Name, taskDto.Priority);
            taskDto.Priority = priority;
            (newName, var deadline) = await FindDeadline(newName, taskDto.Deadline);
            taskDto.Deadline = deadline;
            taskDto.Name = Regex.Replace(newName, @"\s+", " ").Trim();

            var task = taskDto.ToTaskFromCreateDto();
            await _context.TaskList.AddAsync(task);
            await _context.SaveChangesAsync();
            return new OkObjectResult(task.ToTaskDto());
        }

        private async Task<(string, Priority?)> FindPriority(string name, Priority? currentPriority) 
        {
            var priorities = new Dictionary<string, Priority>
            {
                { "!4", Priority.Low },
                { "!3", Priority.Medium },
                { "!2", Priority.High },
                { "!1", Priority.Critical }

            };
            Priority? priority = null;
            //проходимся по приоритетам по возрастанию приоритета, в итоге остается наибольший
            foreach (var pair in priorities) {  
                if (name.Contains(pair.Key)) {
                    priority = pair.Value;
                    name = name.Replace(pair.Key, "");
                }
            }
            
            if (currentPriority == null) {
                return (name, priority);
            }
            return (name, currentPriority);
        }

        private async Task<(string, DateTime?)> FindDeadline(string name, DateTime? currentDeadline) 
        {
            Regex regex = new Regex(@"!before\s((0[1-9]|[12][0-9]|3[01])[-.](0[1-9]|1[0-2])[-.](\d{4}))");
            MatchCollection matches = regex.Matches(name);

            List<DateTime> validDates = new List<DateTime>();
            List<string> validMacros = new List<string>();
            foreach (Match match in matches)
            {
                string dateString = match.Groups[1].Value;
                if (TryParseDate(dateString, out DateTime validDate)) {
                    validDates.Add(validDate);
                    validMacros.Add(match.Value);
                }
            }

            foreach (var validMacro in validMacros)
            {
                name = name.Replace(validMacro, "").Trim();
            }

            if (validDates.Count > 0 && currentDeadline == null) {
                return (name, validDates.Min());
            }
            return (name, currentDeadline);
        }

        private bool TryParseDate(string dateString, out DateTime date)
        {
            return DateTime.TryParseExact(dateString, new[] { "dd.MM.yyyy", "dd-MM-yyyy" }, 
                                        System.Globalization.CultureInfo.InvariantCulture, 
                                        System.Globalization.DateTimeStyles.None, 
                                        out date);
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
            
            task.Description = taskDto.Description;
            task.UpdatedTime = DateTime.UtcNow;

            var (newName, priority) = await FindPriority(taskDto.Name, taskDto.Priority);
            task.Priority = priority ?? Priority.Medium;
            (newName, var deadline) = await FindDeadline(newName, taskDto.Deadline);
            task.Deadline = deadline;
            task.Name = Regex.Replace(newName, @"\s+", " ").Trim();
            
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