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
                Text = taskModel.Text,
                IsChecked = taskModel.IsChecked
            };
        }

        public static TaskElement ToTaskFromCreateDto(this CreateTaskRequestDto taskDto)
        {
            return new TaskElement
            {
                Text = taskDto.Text,
                IsChecked = taskDto.IsChecked
            };
        }


    }
}