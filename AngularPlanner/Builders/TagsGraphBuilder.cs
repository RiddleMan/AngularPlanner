using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AngularPlanner.Dto;
using AngularPlanner.Models;

namespace AngularPlanner.Builders
{
    public class TagsGraphBuilder
    {
        private int Depth { get; set; }
        public TagsGraphBuilder(AngularPlannerContext db)
        {
            _db = db;
            Depth = 5;

        }
        
        /// <summary>
        /// Generating Sunburst of using tags. Veeeeerry slow       
        /// </summary>
        /// <param name="depth">Current call depth</param>
        /// <param name="tags">List of tags to review</param>
        /// <param name="ommit">Tags in current call to ommit</param>
        /// <returns></returns>
        public List<TagsGraphDto> Generate(int depth, List<TagModel> tags, List<int> ommit = null)
        {
            if (ommit == null)
            {
                ommit = new List<int>();
            }

            if (depth == 0)
            {
                return null;
            }

            var tagModels = new List<TagsGraphDto>();

            var allCount = 0;
            decimal allCost = 0;

            tags.ForEach(i =>
            {
                if (!ommit.Contains(i.Id))
                {
                    var ommits = new List<int>();
                    ommits.AddRange(ommit);
                    ommits.Add(i.Id);

                    var model = new TagsGraphDto
                    {
                        Name = i.Name,
                        Id = i.Id
                    };
                    var expenses = _db.Expenses.Where(
                                j =>
                                    j.Tags.Count(k => ommits.Contains(k.Id)) == ommits.Count && j.Cost < 0).Select(k => k.Cost).ToList();

                    allCount += expenses.Count;
                    allCost += expenses.Sum();

                    if (depth == 1)
                    {
                        model.Count = expenses.Count;
                        model.Cost = expenses.Sum();
                    }

                    model.TagGraphs = Generate(depth - 1, tags, ommits);

                    tagModels.Add(model);
                }
            });

            var end = _db.Expenses.Where(
                        j =>
                            j.Tags.Count(k => ommit.Contains(k.Id)) == ommit.Count && j.Cost < 0).Select(k => k.Cost).ToList();

            var endModel = new TagsGraphDto
            {
                Name = "Koniec",
                Id = -1
            };

            if (end.Count - allCount > 0)
            {
                endModel.Count = end.Count - allCount;
            }

            if (end.Sum() - allCost > 0)
            {
                endModel.Cost = end.Sum() - allCost;
            }

            tagModels.Add(endModel);

            return tagModels;
        } 

        public async Task<List<TagsGraphDto>> Build(string userId)
        {
            var tags = await _db.Tags.Where(i => i.UserId == userId).ToListAsync();
            return Generate(Depth, tags);
        } 

        private AngularPlannerContext _db { get; set; }
    }
}