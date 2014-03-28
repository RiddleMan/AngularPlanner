using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AngularPlanner.Dto;
using AngularPlanner.Models;

namespace AngularPlanner.Controllers.Graphs
{
    public class IncomesCostsMonthScopeGraphController : ApiController
    {
        private AngularPlannerContext _db = new AngularPlannerContext();

        public async Task<IncomesCostsMonthScopeDto> Get()
        {
            var now = DateTime.Now.AddMonths(-1);

            var expenses = await _db.Expenses.Where(i => i.DateOfExpense >= now)
                .OrderBy(i => i.DateOfExpense)
                .ThenBy(i => i.DateAdded)
                .Select(i => new
                {
                    Id = i.Id,
                    Cost = i.Cost,
                    Title = i.Title
                })
                .ToListAsync();

            decimal costAfterOp = 0;

            var result = new IncomesCostsMonthScopeDto
            {
                Titles = new List<string>(),
                Costs = new List<IncomesCostsMonthScopeSingleCost>()
            };

            expenses.ForEach(i =>
            {
                costAfterOp += i.Cost;
                result.Titles.Add(i.Title);
                result.Costs.Add(new IncomesCostsMonthScopeSingleCost
                {
                    CostAfterOp = costAfterOp,
                    Id = i.Id
                });
            });

            return result;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
