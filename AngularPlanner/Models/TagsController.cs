﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace AngularPlanner.Models
{
    public class TagsController : ApiController
    {
        private readonly AngularPlannerContext _db;

        public TagsController()
        {
            _db = new AngularPlannerContext();
        }

        public async Task<List<TagModel>> Get()
        {
            var userId = User.Identity.GetUserId();

            return await _db.Tags.Where(i => i.UserId == userId).ToListAsync();
        }

        //Not needed
        //public async Task<TagModel> Get(int id)
        //{
        //    var userId = User.Identity.GetUserId();
        //    return await _db.Tags.Where(i => i.UserId == userId && i.Id == id).FirstOrDefaultAsync();
        //}

        public async Task<HttpResponseMessage> Post([FromBody]TagModel tag)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Tags.Add(tag);
                    tag.UserId = User.Identity.GetUserId();

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

        //Not needed
        //public async Task<HttpResponseMessage> Put([FromBody]ExpenseModel tag)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _db.Expenses.AddOrUpdate(tag);
        //            await _db.SaveChangesAsync();
        //        }
        //        catch (Exception e)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        //        }
        //        return Request.CreateResponse(HttpStatusCode.Created);
        //    }

        //    return Request.CreateResponse(HttpStatusCode.BadRequest);
        //}

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