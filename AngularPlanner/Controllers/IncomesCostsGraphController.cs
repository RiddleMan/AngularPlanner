using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AngularPlanner.Dto;
using AngularPlanner.Models;
using Elmah.Contrib.WebApi;
using WebGrease.Css.Extensions;

namespace AngularPlanner.Controllers
{
    [Authorize]
    [ElmahHandleErrorApi]
    public class IncomesCostsGraphController : ApiController
    {
        private AngularPlannerContext _db = new AngularPlannerContext();
        //
        // GET: /IncomesCostsGraph/
        public async Task<IncomesCostsGraphDto> Get()
        {
            var now = DateTime.Now;
            now = now.AddMilliseconds(-now.Millisecond)
                .AddMinutes(-now.Minute)
                .AddHours(-now.Hour)
                .AddDays(now.Day)
                .AddMonths(1)
                .AddYears(-1);

            var expenses = await _db.Expenses.Where(i => i.DateOfExpense > now).ToListAsync();

            var grouppedExpenses = expenses.GroupBy(i => String.Format("{0:MM-yyyy}", i.DateOfExpense))
                .Select(i => new
                {
                    Date = i.Key,
                    Incomes = i.Where(j => j.Cost >= 0).Sum(j => j.Cost),
                    Costs = -i.Where(j => j.Cost < 0).Sum(j => j.Cost)
                });

            return new IncomesCostsGraphDto
            {
                Dates = grouppedExpenses.Select(i => i.Date),
                Incomes = grouppedExpenses.Select(i => i.Incomes),
                Costs = grouppedExpenses.Select(i => i.Costs)
            };
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