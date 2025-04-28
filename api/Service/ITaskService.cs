using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Models;
using api.Models.Query;
using Microsoft.AspNetCore.Mvc;

namespace api.Service
{
    public interface ITaskService
    {
        Task<IActionResult> CreateTask(CreateTaskDto taskDto);
        Task<IActionResult> ToggleTask(Guid id);
        Task<IActionResult> EditTask(UpdateTaskDto taskDto, Guid id);
        Task<IActionResult> GetAllTasks(Query query);
        Task<IActionResult> DeleteTask(Guid id);
        Task<TaskElement?> FindTask(Guid id);
        Task<IActionResult> GetFullTask(Guid id);
    }
}