using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.Query
{
    public class Query
    {
        public string? Name { get; set; }
        public Status? Status { get; set; } 
        [DefaultValue(Models.Query.Sorting.CreateDateDesc)]
        public Sorting? Sorting { get; set; } = Models.Query.Sorting.CreateDateDesc;
    }
}