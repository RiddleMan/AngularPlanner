using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularPlanner.Dto
{
    public class TagsGraphDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal Cost { get; set; }
        public List<TagsGraphDto> TagGraphs { get; set; }
    }
}