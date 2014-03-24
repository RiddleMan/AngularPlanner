namespace AngularPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpensesAndTags2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TagModels", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TagModels", "UserId", c => c.Int(nullable: false));
        }
    }
}
