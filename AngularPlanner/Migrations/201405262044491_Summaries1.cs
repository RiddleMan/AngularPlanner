namespace AngularPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Summaries1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SummaryModelTagModels",
                c => new
                {
                    SummaryModel_Id = c.Int(nullable: false),
                    TagModel_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.SummaryModel_Id, t.TagModel_Id })
                .ForeignKey("dbo.SummaryModels", t => t.SummaryModel_Id, cascadeDelete: true)
                .ForeignKey("dbo.TagModels", t => t.TagModel_Id, cascadeDelete: true)
                .Index(t => t.SummaryModel_Id)
                .Index(t => t.TagModel_Id);

            DropForeignKey("dbo.TagModels", "SummaryModel_Id", "dbo.SummaryModels");
            DropIndex("dbo.TagModels", new[] { "SummaryModel_Id" });
            DropColumn("dbo.TagModels", "SummaryModel_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TagModels", "SummaryModel_Id", c => c.Int());
            DropForeignKey("dbo.SummaryModelTagModels", "TagModel_Id", "dbo.TagModels");
            DropForeignKey("dbo.SummaryModelTagModels", "SummaryModel_Id", "dbo.SummaryModels");
            DropIndex("dbo.SummaryModelTagModels", new[] { "TagModel_Id" });
            DropIndex("dbo.SummaryModelTagModels", new[] { "SummaryModel_Id" });
            CreateIndex("dbo.TagModels", "SummaryModel_Id");
            AddForeignKey("dbo.TagModels", "SummaryModel_Id", "dbo.SummaryModels", "Id");
            RenameTable(name: "dbo.SummaryModelTagModels", newName: "TagModels");
        }
    }
}
