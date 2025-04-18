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
        [MinLength(4, ErrorMessage = "Task's name should be at least 4 characters")]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsChecked {get; set;} = false;
        public Priority? Priority { get; set; }
        public DateTime? Deadline { get; set; }
    }
}