using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AngularPlanner.Models
{
    public class AngularPlannerContext : DbContext
    {
        public DbSet<ExpenseModel> Expenses { get; set; }
        public DbSet<TagModel> Tags { get; set; }
    }
}