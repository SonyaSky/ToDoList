using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dtos
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } 
        public string? Description { get; set; }
        public bool IsChecked {get; set;} 
        public Priority Priority { get; set; } 
        public Status Status { get; set; } = Status.Active;
        public DateTime? Deadline { get; set; }
    }
}