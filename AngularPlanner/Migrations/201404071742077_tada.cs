namespace AngularPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tada : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SummaryModels", "Scope", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SummaryModels", "Scope");
        }
    }
}
