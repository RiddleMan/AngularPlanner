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
    public class LimitsGraphController : ApiController
    {
        private AngularPlannerContext _db = new AngularPlannerContext();
        //
        // GET: /IncomesCostsGraph/
        [ResponseType(typeof(LimitsGraphDto))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var userId = User.Identity.GetUserId();
            var limit = await _db.Limits.FindAsync(id);

            if(limit == null || limit.UserId != userId)
            {
                return NotFound();
            }

            await _db.Entry(limit).Collection(i => i.Tags).LoadAsync();

            var to = limit.To.AddDays(1);
            var tagIds = limit.Tags.Select(i => i.Id);

            decimal value;

            try
            {
                value = await _db.Expenses.Where(
                    i => i.DateOfExpense >= limit.From && i.DateOfExpense <= to && i.UserId == userId
                         && i.Tags.Any(j => tagIds.Contains(j.Id)))
                         .Distinct().SumAsync(i => i.Cost);
            }
            catch (InvalidOperationException)
            {
                value = 0;
            } 

            return Ok(new LimitsGraphDto
            {
                Id = id,
                Value = -value
            });
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
