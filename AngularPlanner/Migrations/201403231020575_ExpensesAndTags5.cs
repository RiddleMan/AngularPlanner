namespace AngularPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpensesAndTags5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExpenseModels", "DateModified", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExpenseModels", "DateModified");
        }
    }
}
