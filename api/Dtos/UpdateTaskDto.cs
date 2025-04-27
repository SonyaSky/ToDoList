using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dtos
{
    public class UpdateTaskDto
    {
        [Required]
        [MinLength(4, ErrorMessage = "Task's name should be at least 4 characters")]
        public required string Name { get; set; } 
        public string? Description { get; set; }
        public Priority? Priority { get; set; } 
        public DateTime? Deadline { get; set; }
    }
}