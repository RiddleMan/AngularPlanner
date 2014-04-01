namespace AngularPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Limits : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LimitModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LimitModelTagModels",
                c => new
                    {
                        LimitModel_Id = c.Int(nullable: false),
                        TagModel_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LimitModel_Id, t.TagModel_Id })
                .ForeignKey("dbo.LimitModels", t => t.LimitModel_Id, cascadeDelete: true)
                .ForeignKey("dbo.TagModels", t => t.TagModel_Id, cascadeDelete: true)
                .Index(t => t.LimitModel_Id)
                .Index(t => t.TagModel_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LimitModelTagModels", "TagModel_Id", "dbo.TagModels");
            DropForeignKey("dbo.LimitModelTagModels", "LimitModel_Id", "dbo.LimitModels");
            DropIndex("dbo.LimitModelTagModels", new[] { "TagModel_Id" });
            DropIndex("dbo.LimitModelTagModels", new[] { "LimitModel_Id" });
            DropTable("dbo.LimitModelTagModels");
            DropTable("dbo.LimitModels");
        }
    }
}
