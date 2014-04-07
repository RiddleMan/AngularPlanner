using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AngularPlanner.Models
{
    public class AngularPlannerContext : DbContext
    {
        public AngularPlannerContext()
            : base(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString())
        {
            
        }

        public DbSet<ExpenseModel> Expenses { get; set; }
        public DbSet<TagModel> Tags { get; set; }
        public DbSet<LimitModel> Limits { get; set; }
        public DbSet<SummaryModel> Summaries { get; set; }
    }
}