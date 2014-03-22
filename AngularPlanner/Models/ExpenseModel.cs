using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngularPlanner.Models
{
    public class ExpenseModel
    {
        [Key]
        public int Id { get; set; }
        public List<TagModel> Tags { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public decimal Cost { get; set; }
        public string UserId { get; set; }
        public Nullable<DateTime> DateAdded { get; set; }
        public Nullable<DateTime> DateOfExpense { get; set; }
    }
}