using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AngularPlanner.Models
{
    public class LimitModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public List<TagModel> Tags { get; set; }
        public string UserId { get; set; }
    }
}