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

namespace AngularPlanner.Controllers
{
    public class IncomesCostsBilanceGraphController : ApiController
    {
        private AngularPlannerContext _db = new AngularPlannerContext();
        //
        // GET: /IncomesCostsGraph/
        public async Task<IncomesCostsBilanceGraphDto> Get()
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
                    Bilances = i.Sum(j => j.Cost)
                });

            return new IncomesCostsBilanceGraphDto
            {
                Dates = grouppedExpenses.Select(i => i.Date),
                Bilances = grouppedExpenses.Select(i => i.Bilances)
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
