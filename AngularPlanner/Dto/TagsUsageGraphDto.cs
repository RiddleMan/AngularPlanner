using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AngularPlanner.Models;

namespace AngularPlanner.Dto
{
    public class TagsUsageGraphDto
    {
        public List<string> Tags { get; set; }
        public List<int> Usage { get; set; } 
    }
}