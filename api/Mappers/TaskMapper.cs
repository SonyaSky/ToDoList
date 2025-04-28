using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Dtos;

namespace api.Mappers
{
    public static class TaskMapper
    {
        public static TaskDto ToTaskDto(this TaskElement taskModel)
        {
            var task = new TaskDto
            {
                Id = taskModel.Id,
                Name = taskModel.Name,
                IsChecked = taskModel.IsChecked,
                Description = taskModel.Description,
                Deadline = taskModel.Deadline,
                Priority = taskModel.Priority,
            };
            task.Status = taskModel.GetStatus();
            return task;
        }

        public static TaskElement ToTaskFromCreateDto(this CreateTaskDto taskModel)
        {
            return new TaskElement
            {
                Id = new Guid(),
                Name = taskModel.Name,
                Description = taskModel.Description,
                Deadline = taskModel.Deadline,
                Priority = taskModel.Priority ?? Priority.Medium
            };
        }

        public static FullTaskDto ToFullTaskDto(this TaskElement taskModel)
        {
            var task = new FullTaskDto
            {
                Id = taskModel.Id,
                Name = taskModel.Name,
                IsChecked = taskModel.IsChecked,
                Description = taskModel.Description,
                Deadline = taskModel.Deadline,
                Priority = taskModel.Priority,
                CreateTime = taskModel.CreateTime,
                UpdatedTime = taskModel.UpdatedTime
            };
            task.Status = taskModel.GetStatus();
            return task;
        }
        public static Status GetStatus(this TaskElement task) 
        {
            if (DateTime.UtcNow > task.Deadline) {
                if (task.IsChecked) {
                    return Status.Late;
                } else {
                    return Status.Overdue;
                }
            } else {
                if (task.IsChecked) {
                    return Status.Completed;
                } else {
                    return Status.Active;
                }
            }
        }


    }
}