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
    
    [Route("api/TaskElement")]
    [ApiController]

    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, Accept-Language, Accept-Encoding");
            var taskList = await _taskService.GetAllTasks();

            return Ok(taskList);
        }

        [HttpPost]
        [Route("post")]
        public async Task<IActionResult> Create([FromBody] CreateTaskDto taskDto)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, Accept-Language, Accept-Encoding");
            
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var taskModel = await _taskService.CreateTask(taskDto);
            if (taskModel == null) {
                return BadRequest("Couldn't create task");
            }
            return Ok(taskModel);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateTaskDto updateDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var taskModel = await _taskService.FindTask(id);
            if (taskModel == null)
            {
                return NotFound();
            }

            var updatedTask = await _taskService.EditTask(updateDto, id);
            if (updatedTask == null) {
                return BadRequest("Couldn't edit task");
            }
            return Ok(updatedTask);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, Accept-Language, Accept-Encoding");
            var taskModel = await _taskService.FindTask(id);
            if (taskModel == null)
            {
                return NotFound();
            }

            var idDeleted = await _taskService.DeleteTask(id);
            if (!idDeleted) {
                return BadRequest("Couldn't delete task");
            }
            return Ok();
        }


        [HttpPatch]
        [Route("{id}/toggle")]
        public async Task<IActionResult> ToggleCheck([FromRoute] Guid id)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS, PATCH");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, Accept-Language, Accept-Encoding");

            if (!ModelState.IsValid) return BadRequest(ModelState);
            var taskModel = await _taskService.FindTask(id);
            if (taskModel == null)
            {
                return NotFound();
            }

            var updatedTask = await _taskService.ToggleTask(id);
            if (updatedTask == null) {
                return BadRequest("Couldn't toggle task");
            }
            return Ok(updatedTask);
        }


    }
}