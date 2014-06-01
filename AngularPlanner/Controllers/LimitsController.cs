using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AngularPlanner.Models;
using Elmah.Contrib.WebApi;
using Microsoft.AspNet.Identity;

namespace AngularPlanner.Controllers
{
    [Authorize]
    [ElmahHandleErrorApi]
    public class LimitsController : ApiController
    {
        private AngularPlannerContext db = new AngularPlannerContext();

        // GET api/Limits
        public async Task<List<LimitModel>> GetLimits()
        {
            var userId = User.Identity.GetUserId();
            return await db.Limits.Where(i => i.UserId == userId).Include(i => i.Tags).ToListAsync();
        }

        // GET api/Limits/5
        [ResponseType(typeof(LimitModel))]
        public async Task<IHttpActionResult> GetLimitModel(int id)
        {
            var userId = User.Identity.GetUserId();
            LimitModel limitmodel = await db.Limits.FindAsync(id);
            if (limitmodel == null || limitmodel.UserId != userId)
            {
                return NotFound();
            }

            return Ok(limitmodel);
        }

        // PUT api/Limits/5
        public async Task<IHttpActionResult> PutLimitModel(int id, LimitModel limitmodel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != limitmodel.Id)
            {
                return BadRequest();
            }

            var limit = db.Limits.Find(id);
            await db.Entry(limit).Collection(i => i.Tags).LoadAsync();

            if (limit.UserId != User.Identity.GetUserId())
            {
                return BadRequest();
            }

            var tagIds = limitmodel.Tags.Select(i => i.Id);
            var currentTagIds = limit.Tags.Select(i => i.Id);
            var toAdd = tagIds.Where(i => !currentTagIds.Contains(i));

            limit.Tags.RemoveAll(i => !tagIds.Contains(i.Id));
            limit.Tags.AddRange(db.Tags.Where(i => toAdd.Contains(i.Id)).ToList());
            limit.Name = limitmodel.Name;
            limit.To = limitmodel.To;
            limit.From = limitmodel.From;
            limit.Amount = limitmodel.Amount;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LimitModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Limits
        [ResponseType(typeof(LimitModel))]
        public async Task<IHttpActionResult> PostLimitModel(LimitModel limitmodel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tagIds = limitmodel.Tags.Select(i => i.Id);

            limitmodel.UserId = User.Identity.GetUserId();
            limitmodel.Tags = await db.Tags.Where(i => tagIds.Contains(i.Id)).ToListAsync();

            db.Limits.Add(limitmodel);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = limitmodel.Id }, limitmodel);
        }

        // DELETE api/Limits/5
        [ResponseType(typeof(LimitModel))]
        public async Task<IHttpActionResult> DeleteLimitModel(int id)
        {
            var userId = User.Identity.GetUserId();
            LimitModel limitmodel = await db.Limits.FindAsync(id);
            if (limitmodel == null || limitmodel.UserId != userId)
            {
                return NotFound();
            }

            db.Limits.Remove(limitmodel);
            await db.SaveChangesAsync();

            return Ok(limitmodel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LimitModelExists(int id)
        {
            return db.Limits.Count(e => e.Id == id) > 0;
        }
    }
}