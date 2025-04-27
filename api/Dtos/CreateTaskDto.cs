using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dtos
{
    public class CreateTaskDto
    {
        [Required]
        [MinLength(4, ErrorMessage = "Task's name should be at least 4 characters long")]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public Priority? Priority { get; set; }
        public DateTime? Deadline { get; set; }
    }
}