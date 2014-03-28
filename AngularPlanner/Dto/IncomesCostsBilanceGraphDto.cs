using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularPlanner.Dto
{
    public class IncomesCostsBilanceGraphDto
    {
        public IEnumerable<decimal> Bilances { get; set; }
        public IEnumerable<string> Dates { get; set; } 
    }
}