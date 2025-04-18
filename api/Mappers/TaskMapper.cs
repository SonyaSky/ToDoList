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
            return new TaskDto
            {
                Id = taskModel.Id,
                Name = taskModel.Name,
                IsChecked = taskModel.IsChecked,
                Description = taskModel.Description,
                Deadline = taskModel.Deadline,
                Priority = taskModel.Priority,
            };
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


    }
}