namespace AngularPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Summaries : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SummaryModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.TagModels", "SummaryModel_Id", c => c.Int());
            CreateIndex("dbo.TagModels", "SummaryModel_Id");
            AddForeignKey("dbo.TagModels", "SummaryModel_Id", "dbo.SummaryModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagModels", "SummaryModel_Id", "dbo.SummaryModels");
            DropIndex("dbo.TagModels", new[] { "SummaryModel_Id" });
            DropColumn("dbo.TagModels", "SummaryModel_Id");
            DropTable("dbo.SummaryModels");
        }
    }
}
