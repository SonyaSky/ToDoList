using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Dtos;
using api.Mappers; 
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using api.Models;
using Microsoft.AspNetCore.Cors;
using api.Service;

namespace api.Controllers
{
    
    [Route("api/task")]
    [ApiController]

    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, Accept-Language, Accept-Encoding");

            return await _taskService.GetAllTasks();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaskDto taskDto)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, Accept-Language, Accept-Encoding");
            
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return await _taskService.CreateTask(taskDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateTaskDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return await _taskService.EditTask(updateDto, id);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, Accept-Language, Accept-Encoding");
            
            return await _taskService.DeleteTask(id);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask([FromRoute] Guid id) {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, Accept-Language, Accept-Encoding");
            
            return await _taskService.GetFullTask(id);
        }


        [HttpPatch("{id}/toggle")]
        public async Task<IActionResult> ToggleCheck([FromRoute] Guid id)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS, PATCH");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, Accept-Language, Accept-Encoding");

            return await _taskService.ToggleTask(id);
        }


    }
}