using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularPlanner.Dto
{
    public class SummariesGraphDto
    {
        public List<string> Series  { get; set; }
        public List<decimal> Values { get; set; }
    }
}