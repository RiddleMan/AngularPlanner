namespace AngularPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpensesAndTags3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExpenseModels", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExpenseModels", "Title");
        }
    }
}
