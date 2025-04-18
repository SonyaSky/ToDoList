using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dtos
{
    public class FullTaskDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; } 
        public string? Description { get; set; }
        public bool IsChecked {get; set;} 
        public Priority Priority { get; set; } 
        public Status Status { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime CreateTime { get; set; } 
        public DateTime? UpdatedTime { get; set; } 
    }
}