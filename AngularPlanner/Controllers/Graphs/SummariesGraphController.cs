using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AngularPlanner.Dto;
using AngularPlanner.Models;
using Elmah.Contrib.WebApi;
using Microsoft.AspNet.Identity;

namespace AngularPlanner.Controllers.Graphs
{
    [Authorize]
    [ElmahHandleErrorApi]
    public class SummariesGraphController : ApiController
    {
        private readonly AngularPlannerContext _db = new AngularPlannerContext();
        //
        // GET: /IncomesCostsGraph/
        [ResponseType(typeof(SummariesGraphDto))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var summary = await _db.Summaries.FindAsync(id);

            if (summary == null || summary.UserId != User.Identity.GetUserId())
            {
                return NotFound();
            }

            string format;

            switch (summary.Scope)
            {
                case SummaryScope.Daily:
                    format = "{0:dd-MM-yyyy}";
                    break;
                case SummaryScope.Yearly:
                    format = "{0:yyyy}";
                    break;
                default:
                    format = "{0:MM-yyyy}";
                    break;
            }

            var expenses = await _db.Expenses.Where(i => i.DateOfExpense >= summary.From && i.DateOfExpense <= summary.To).ToListAsync();

            var grouppedExpenses = expenses.GroupBy(i => String.Format(format, i.DateOfExpense))
                .Select(i => new
                {
                    Date = i.Key,
                    Bilances = i.Sum(j => j.Cost)
                });

            return Ok(new SummariesGraphDto());
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
