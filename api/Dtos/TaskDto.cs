using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class TaskDto
    {
        public int Id { get; set; }

        public string Text { get; set; } = string.Empty;

        public bool IsChecked {get; set;}
    }
}