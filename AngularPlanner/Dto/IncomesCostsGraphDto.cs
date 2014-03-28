using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularPlanner.Dto
{
    public class IncomesCostsGraphDto
    {
        public IEnumerable<decimal> Incomes { get; set; }
        public IEnumerable<decimal> Costs { get; set; }
        public IEnumerable<string> Dates { get; set; } 
    }
}