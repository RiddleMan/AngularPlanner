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
using AngularPlanner.Builders;
using AngularPlanner.Dto;
using AngularPlanner.Models;
using Elmah.Contrib.WebApi;
using Microsoft.AspNet.Identity;

namespace AngularPlanner.Controllers
{
    [Authorize]
    [ElmahHandleErrorApi]
    public class TagsGraphsController : ApiController
    {
        private AngularPlannerContext db = new AngularPlannerContext();
        private readonly TagsGraphBuilder _builder;

        public TagsGraphsController()
        {
            _builder = new TagsGraphBuilder(db);
        }

        //Get tag stats
        // GET api/TagsGraph
        // LOW !!!! PERFORMANCE
        //public async Task<IEnumerable<TagsGraphDto>> GetTags()
        //{
        //    var userId = User.Identity.GetUserId();
        //    return await _builder.Build(userId);
        //}
        [ActionName("UsingStatistics")]
        public async Task<TagsGraphsUsingStatisticsDto> GetUsingStatistics()
        {
            var userId = User.Identity.GetUserId();

            var stats = await db.Tags.Where(i => i.UserId == userId).Select(i => new
            {
                Name = i.Name,
                Count = i.Expenses.Count
            }).ToListAsync();

            var noTag = await db.Expenses.CountAsync(i => !i.Tags.Any() && i.UserId == userId);

            var ret = new TagsGraphsUsingStatisticsDto
            {
                Tags = stats.Select(i => i.Name).ToList(),
                Usage = stats.Select(i => i.Count).ToList()
            };

            ret.Tags.Add("Bez taga");
            ret.Usage.Add(noTag);

            return ret;
        }

        //// GET api/TagsGraph/5
        //[ResponseType(typeof(TagModel))]
        //public async Task<IHttpActionResult> GetTagModel(int id)
        //{
        //    TagModel tagmodel = await db.Tags.FindAsync(id);
        //    if (tagmodel == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(tagmodel);
        //}

        //// PUT api/TagsGraph/5
        //public async Task<IHttpActionResult> PutTagModel(int id, TagModel tagmodel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != tagmodel.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(tagmodel).State = EntityState.Modified;

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TagModelExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST api/TagsGraph
        //[ResponseType(typeof(TagModel))]
        //public async Task<IHttpActionResult> PostTagModel(TagModel tagmodel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Tags.Add(tagmodel);
        //    await db.SaveChangesAsync();

        //    return CreatedAtRoute("DefaultApi", new { id = tagmodel.Id }, tagmodel);
        //}

        //// DELETE api/TagsGraph/5
        //[ResponseType(typeof(TagModel))]
        //public async Task<IHttpActionResult> DeleteTagModel(int id)
        //{
        //    TagModel tagmodel = await db.Tags.FindAsync(id);
        //    if (tagmodel == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Tags.Remove(tagmodel);
        //    await db.SaveChangesAsync();

        //    return Ok(tagmodel);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //private bool TagModelExists(int id)
        //{
        //    return db.Tags.Count(e => e.Id == id) > 0;
        //}
    }
}