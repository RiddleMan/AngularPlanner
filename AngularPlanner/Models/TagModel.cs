﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AngularPlanner.Models
{
    public class TagModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ExpenseModel> Expenses { get; set; }
        public string UserId { get; set; }
        public List<LimitModel> Limits { get; set; }
        public List<SummaryModel> Summaries { get; set; } 
    }
}