using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AngularPlanner.Models
{
    public class SummaryModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public List<TagModel> Tags { get; set; }
        public SummaryType Type { get; set; }
        public SummaryScope Scope { get; set; }
        public string UserId { get; set; }
    }

    public enum SummaryScope
    {
        Daily,
        Monthly,
        Yearly
    }

    public enum SummaryType
    {
        Linear,
        Column,
        Circle
    }
}