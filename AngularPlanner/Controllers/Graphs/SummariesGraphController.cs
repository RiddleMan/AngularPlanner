using System;
using System.Data.Entity;
using System.Linq;
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

            await _db.Entry(summary).Collection(i => i.Tags).LoadAsync();

            var tagIds = summary.Tags.Select(i => i.Id);

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

            var expenses = await _db.Expenses.Where(i => i.DateOfExpense >= summary.From && i.DateOfExpense <= summary.To && i.Tags.Any(j => tagIds.Contains(j.Id))).Distinct().ToListAsync();

            var grouppedExpenses = expenses.GroupBy(i => String.Format(format, i.DateOfExpense)).ToList();

            var summaryDto = new SummariesGraphDto
            {
                Values = grouppedExpenses.Select(i => i.Sum(j => j.Cost)).ToList(),
                Series = grouppedExpenses.Select(i => i.Key).ToList()
            };

            return Ok(summaryDto);
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
