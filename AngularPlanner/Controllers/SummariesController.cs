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
    public class SummariesController : ApiController
    {
        private readonly AngularPlannerContext _db = new AngularPlannerContext();

        // GET api/Limits
        public async Task<List<SummaryModel>> GetSummaries()
        {
            var userId = User.Identity.GetUserId();
            return await _db.Summaries.Where(i => i.UserId == userId).Include(i => i.Tags).ToListAsync();
        }

        // GET api/Limits/5
        [ResponseType(typeof(SummaryModel))]
        public async Task<IHttpActionResult> GetSummaryModel(int id)
        {
            var userId = User.Identity.GetUserId();
            LimitModel summaryModel = await _db.Limits.FindAsync(id);
            if (summaryModel == null || summaryModel.UserId != userId)
            {
                return NotFound();
            }

            return Ok(summaryModel);
        }

        // PUT api/Limits/5
        public async Task<IHttpActionResult> PutSummaryModel(int id, SummaryModel summaryModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != summaryModel.Id)
            {
                return BadRequest();
            }

            summaryModel.UserId = User.Identity.GetUserId();
            _db.Entry(summaryModel).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SummaryModelExists(id))
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
        [ResponseType(typeof(SummaryModel))]
        public async Task<IHttpActionResult> PostSummaryModel(SummaryModel summaryModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var tagIds = summaryModel.Tags.Select(i => i.Id);

            summaryModel.UserId = User.Identity.GetUserId();
            summaryModel.Tags = await _db.Tags.Where(i => tagIds.Contains(i.Id)).ToListAsync();

            _db.Summaries.Add(summaryModel);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = summaryModel.Id }, summaryModel);
        }

        // DELETE api/Limits/5
        [ResponseType(typeof(SummaryModel))]
        public async Task<IHttpActionResult> DeleteLimitModel(int id)
        {
            var userId = User.Identity.GetUserId();
            SummaryModel summaryModel = await _db.Summaries.FindAsync(id);
            if (summaryModel == null || summaryModel.UserId != userId)
            {
                return NotFound();
            }

            _db.Summaries.Remove(summaryModel);
            await _db.SaveChangesAsync();

            return Ok(summaryModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SummaryModelExists(int id)
        {
            return _db.Summaries.Count(e => e.Id == id) > 0;
        }
    }
}