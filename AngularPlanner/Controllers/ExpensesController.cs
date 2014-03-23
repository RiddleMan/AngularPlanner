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
        public async Task<List<ExpenseModel>> GetList(int page = 1)
        {
            var userId = User.Identity.GetUserId();

            try
            {
                return await _db.Expenses
                    .Where(i => i.UserId == userId)
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
                try
                {
                    var tagIds = expense.Tags.Select(i => i.Id);

                    expense.Tags = await _db.Tags.Where(i => tagIds.Contains(i.Id)).ToListAsync();
                    expense.DateAdded = DateTime.Now;
                    expense.UserId = User.Identity.GetUserId();
                    _db.Expenses.Add(expense);

                    await _db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
                return Request.CreateResponse(HttpStatusCode.Created);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        public async Task<HttpResponseMessage> Put([FromBody]ExpenseModel expense)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var tagIds = expense.Tags.Select(i => i.Id);

                    expense.Tags = await _db.Tags.Where(i => tagIds.Contains(i.Id)).ToListAsync();
                    _db.Expenses.AddOrUpdate(expense);
                    await _db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }
                return Request.CreateResponse(HttpStatusCode.Created);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {
            var userId = User.Identity.GetUserId();
            try
            {
                var expense = await _db.Expenses.Where(i => i.UserId == userId && i.Id == id).FirstOrDefaultAsync();
                if (expense == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                _db.Expenses.Remove(expense);
                await _db.SaveChangesAsync();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}