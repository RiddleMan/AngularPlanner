using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AngularPlanner.Helpers;
using AngularPlanner.Models;
using Elmah.Contrib.WebApi;
using Elmah.Mvc;
using Microsoft.AspNet.Identity;

namespace AngularPlanner.Controllers
{
    [Authorize]
    [ElmahHandleErrorApi]
    public class ExpensesController : ApiController
    {
        private readonly AngularPlannerContext _db;

        public ExpensesController()
        {
            _db = new AngularPlannerContext();
        }

        [HttpGet]
        [ActionName("Get")]
        public async Task<List<ExpenseModel>> GetListByDate(string date, int page = 1)
        {
            var userId = User.Identity.GetUserId();

            var timespan = TimeSpanHelper.GetTimeSpan(date);

            try
            {
                return await _db.Expenses
                    .Where(i => i.UserId == userId && i.DateOfExpense >= timespan.Lower && i.DateOfExpense <= timespan.Higher)
                    .Include("Tags")
                    .AsNoTracking()
                    .OrderByDescending(i => i.DateOfExpense)
                    .Skip((page - 1) * 20)
                    .Take(20)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }

            return new List<ExpenseModel>();
        }

        [HttpGet]
        [ActionName("Get")]
        public async Task<List<ExpenseModel>> GetListByTag(string tag, int page = 1)
        {
            var userId = User.Identity.GetUserId();

            try
            {
                var query = _db.Expenses.AsQueryable();

                query = tag == "notag" 
                    ? query.Where(i => i.UserId == userId && !i.Tags.Any()) 
                    : query.Where(i => i.UserId == userId && i.Tags.Any(j => j.Name == tag));

                return await query.Include("Tags")
                    .AsNoTracking()
                    .OrderByDescending(i => i.DateOfExpense)
                    .Skip((page - 1) * 20)
                    .Take(20)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }

            return new List<ExpenseModel>();
        }

        [HttpGet]
        [ActionName("Get")]
        public async Task<List<ExpenseModel>> GetList(int page = 1)
        {
            var userId = User.Identity.GetUserId();

            try
            {
                return await _db.Expenses
                    .Where(i => i.UserId == userId)
                    .Include("Tags")
                    .AsNoTracking()
                    .OrderByDescending(i => i.DateOfExpense)
                    .Skip((page - 1) * 20)
                    .Take(20)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }

            return new List<ExpenseModel>();
        }

        [HttpGet]
        [ActionName("Get")]
        public async Task<ExpenseModel> GetSingle(int id)
        {
            var userId = User.Identity.GetUserId();
            return await _db.Expenses.Where(i => i.UserId == userId && i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<HttpResponseMessage> Post([FromBody]ExpenseModel expense)
        {
            if (ModelState.IsValid)
            {
                var tagIds = expense.Tags.Select(i => i.Id);

                expense.Tags = await _db.Tags.Where(i => tagIds.Contains(i.Id)).ToListAsync();
                expense.DateAdded = DateTime.Now;
                expense.UserId = User.Identity.GetUserId();
                _db.Expenses.Add(expense);

                await _db.SaveChangesAsync();

                return Request.CreateResponse(HttpStatusCode.Created);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        public async Task<HttpResponseMessage> Put([FromBody]ExpenseModel expense)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                var expenseDB =
                    await _db.Expenses.Include(i => i.Tags).FirstOrDefaultAsync(i => i.Id == expense.Id && i.UserId == userId);

                var tagExist = expenseDB.Tags.Select(i => i.Id);
                var tagIds = expense.Tags.Select(i => i.Id);

                expenseDB.Tags.AddRange(await _db.Tags.Where(i => tagIds.Contains(i.Id) && !tagExist.Contains(i.Id)).ToListAsync());
                expenseDB.Tags.RemoveAll(i => !tagIds.Contains(i.Id));
                await _db.SaveChangesAsync();

                return Request.CreateResponse(HttpStatusCode.Created);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {
            var userId = User.Identity.GetUserId();
            var expense = await _db.Expenses.Where(i => i.UserId == userId && i.Id == id).FirstOrDefaultAsync();
            if (expense == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            _db.Expenses.Remove(expense);
            await _db.SaveChangesAsync();

            return Request.CreateResponse(HttpStatusCode.OK);
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