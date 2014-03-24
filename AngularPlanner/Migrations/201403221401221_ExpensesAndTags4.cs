namespace AngularPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpensesAndTags4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ExpenseModels", "DateAdded", c => c.DateTime());
            AlterColumn("dbo.ExpenseModels", "DateOfExpense", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ExpenseModels", "DateOfExpense", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ExpenseModels", "DateAdded", c => c.DateTime(nullable: false));
        }
    }
}
