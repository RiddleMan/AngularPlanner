using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AngularPlanner.Models;

namespace AngularPlanner.Dto
{
    public class IncomesCostsMonthScopeDto
    {
        public List<string> Titles { get; set; }
        public List<IncomesCostsMonthScopeSingleCost> Costs { get; set; }
    }

    public class IncomesCostsMonthScopeSingleCost
    {
        public int Id { get; set; }
        public decimal CostAfterOp { get; set; }
    }
}