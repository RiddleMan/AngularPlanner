namespace AngularPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Limits1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LimitModels", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.LimitModels", "UserId");
        }
    }
}
