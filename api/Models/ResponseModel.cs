using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class ResponseModel
    {
        public required string Status { get; set; }
        public required string Message { get; set; }
    }
}