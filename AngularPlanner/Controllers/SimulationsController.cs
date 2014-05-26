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

namespace AngularPlanner.Controllers
{
    [Authorize]
    [ElmahHandleErrorApi]
    public class SimulationsController : ApiController
    {
        private AngularPlannerContext _db { get; set; }

        public SimulationsController()
        {
            _db = new AngularPlannerContext();
        }

        public IEnumerable<decimal> AssignToDates(List<string> dates, List<IGrouping<string, ExpenseModel>> data)
        {
            var result = new List<decimal>();

            dates.ForEach(i =>
            {
                result.Add(
                    data.Any(j => j.Key == i) 
                    ? Math.Abs(data.FirstOrDefault(j => j.Key == i).Sum(k => k.Cost))
                    : 0
                );
            });

            return result;
        }

        [ResponseType(typeof(SimulationsDto))]
        public async Task<IHttpActionResult> Get(SimulationScope scope)
        {
            var result = new SimulationsDto();
            var userId = User.Identity.GetUserId();

            var dateTo = DateTime.Now;
            DateTime dateFrom;

            string format;
            var dates = new List<string>();

            DateTime dateI;
            switch (scope)
            {
                case SimulationScope.Daily:
                    format = "{0:dd-MM-yyyy}";
                    dateFrom = dateTo.AddDays(-30);
                    dateI = dateFrom;
                    for (var i = 0; i < 31; i++)
                    {
                        dates.Add(String.Format(format, dateI));
                        dateI = dateI.AddDays(1);
                    }
                    break;
                case SimulationScope.Yearly:
                    format = "{0:yyyy}";
                    dateFrom = dateTo.AddYears(-2);
                    dateI = dateFrom;
                    for (var i = 0; i < 3; i++)
                    {
                        dates.Add(String.Format(format, dateI));
                        dateI = dateI.AddYears(1);
                    }
                    break;
                default:
                    format = "{0:MM-yyyy}";
                    dateFrom = dateTo.AddMonths(-6);
                    dateI = dateFrom;
                    for (var i = 0; i < 7; i++)
                    {
                        dates.Add(String.Format(format, dateI));
                        dateI = dateI.AddMonths(1);
                    }
                    break;
            }
            
            var expenses = await _db.Expenses.Where(i => i.DateOfExpense >= dateFrom && i.DateOfExpense <= dateTo && i.UserId == userId).Distinct().ToListAsync();

            var groupedPastIncomes =
                expenses.Where(i => i.Cost >= 0).GroupBy(i => String.Format(format, i.DateOfExpense)).ToList();

            var groupedPastOutcomes =
                expenses.Where(i => i.Cost < 0).GroupBy(i => String.Format(format, i.DateOfExpense)).ToList();

            var incomesSum = new List<decimal>();
            groupedPastIncomes.ForEach(i => incomesSum.Add(i.Any() ? i.Sum(j => j.Cost) : 0));

            var outcomesSum = new List<decimal>();
            groupedPastOutcomes.ForEach(i => outcomesSum.Add(i.Any() ? i.Sum(j => j.Cost) : 0));

            var estimatedIncome = incomesSum.Any() ? incomesSum.Average() : 0;
            var estimatedOutcome = outcomesSum.Any() ? outcomesSum.Average(): 0;

            var summaryDto = new SimulationsDto()
            {
                PastIncomes = AssignToDates(dates, groupedPastIncomes),
                PastOutcomes = AssignToDates(dates, groupedPastOutcomes),
                Dates = dates,
                EstimatedIncome = estimatedIncome,
                EstimatedOutcome = -estimatedOutcome
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
