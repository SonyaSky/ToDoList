using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class UpdateTaskRequestDto
    {
        public string Text { get; set; } = string.Empty;
        public bool IsChecked {get; set;}
    }
}