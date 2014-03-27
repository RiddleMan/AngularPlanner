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
    public class TagsUsageGraphController : ApiController
    {
        private AngularPlannerContext db = new AngularPlannerContext();
        private readonly TagsGraphBuilder _builder;

        public TagsUsageGraphController()
        {
            _builder = new TagsGraphBuilder(db);
        }

        public async Task<TagsGraphsUsingStatisticsDto> GetTagsUsageGraph()
        {
            var userId = User.Identity.GetUserId();

            var stats = await db.Tags.Where(i => i.UserId == userId && i.Expenses.Any()).Select(i => new
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}