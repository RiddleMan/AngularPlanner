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

        public async Task<List<ExpenseModel>> Get()
        {
            var userId = User.Identity.GetUserId();

            return await _db.Expenses.Where(i => i.UserId == userId).ToListAsync();
        }

        public async Task<ExpenseModel> Get(int id)
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
                    _db.Expenses.Add(expense);
                    await _db.SaveChangesAsync();
                }
                catch (InvalidOperationException e)
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