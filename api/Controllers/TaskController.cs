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

namespace api.Controllers
{
    
    [Route("api/TaskElement")]
    [ApiController]

    public class TaskController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public TaskController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, Accept-Language, Accept-Encoding");
            var taskList = _context.TaskList.ToList()
              .Select(t => t.ToTaskDto());

            return Ok(taskList);
        }


        
        [HttpPost]
        [Route("post")]
        public IActionResult Create([FromBody] CreateTaskRequestDto taskDto)
        {
            var taskModel = taskDto.ToTaskFromCreateDto();
            _context.TaskList.Add(taskModel);
            _context.SaveChanges();
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, Accept-Language, Accept-Encoding");
            return CreatedAtAction(nameof(GetAll), new { id = taskModel.Id}, taskModel.ToTaskDto());
        }

        // [HttpPost]
        // [Route("list")]
        // public IActionResult CreateMultiple([FromBody] List<CreateTaskRequestDto> taskDtos)
        // {
        //     _context.TaskList.RemoveRange(_context.TaskList);
        //     _context.SaveChanges();
        //     var taskModels = taskDtos.Select(dto => dto.ToTaskFromCreateDto()).ToList();
        //     _context.TaskList.AddRange(taskModels);
        //     _context.SaveChanges();
        //     return CreatedAtAction(nameof(GetAll), taskModels.Select(t => t.ToTaskDto()));
        // }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateTaskRequestDto updateDto)
        {
            var taskModel = _context.TaskList.FirstOrDefault(x => x.Id == id);
            if (taskModel == null)
            {
                return NotFound();
            }

            taskModel.Text = updateDto.Text;
            taskModel.IsChecked = updateDto.IsChecked;
            
            _context.SaveChanges();

            return Ok(taskModel.ToTaskDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, Accept-Language, Accept-Encoding");
            var taskModel = _context.TaskList.FirstOrDefault(x => x.Id == id);
            if (taskModel == null)
            {
                return NotFound();
            }

            _context.TaskList.Remove(taskModel);
            _context.SaveChanges();
            return NoContent();
        }


        [HttpPatch]
        [Route("{id}/toggle")]
        public IActionResult ToggleCheck([FromRoute] int id)
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS, PATCH");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, Accept-Language, Accept-Encoding");

            var taskModel = _context.TaskList.FirstOrDefault(x => x.Id == id);
            if (taskModel == null)
            {
                return NotFound();
            }

            taskModel.IsChecked = !taskModel.IsChecked;
            _context.SaveChanges();
            return Ok(taskModel.ToTaskDto());
        }


    }
}