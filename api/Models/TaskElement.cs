using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class TaskElement
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsChecked {get; set;} = false;
        public Priority Priority { get; set; } = Priority.Medium;
        public DateTime? Deadline { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedTime { get; set; } 
    } 
}