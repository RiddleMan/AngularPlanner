namespace AngularPlanner.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExpensesAndTags1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExpenseModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                        DateOfExpense = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagModelExpenseModels",
                c => new
                    {
                        TagModel_Id = c.Int(nullable: false),
                        ExpenseModel_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TagModel_Id, t.ExpenseModel_Id })
                .ForeignKey("dbo.TagModels", t => t.TagModel_Id, cascadeDelete: true)
                .ForeignKey("dbo.ExpenseModels", t => t.ExpenseModel_Id, cascadeDelete: true)
                .Index(t => t.TagModel_Id)
                .Index(t => t.ExpenseModel_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagModelExpenseModels", "ExpenseModel_Id", "dbo.ExpenseModels");
            DropForeignKey("dbo.TagModelExpenseModels", "TagModel_Id", "dbo.TagModels");
            DropIndex("dbo.TagModelExpenseModels", new[] { "ExpenseModel_Id" });
            DropIndex("dbo.TagModelExpenseModels", new[] { "TagModel_Id" });
            DropTable("dbo.TagModelExpenseModels");
            DropTable("dbo.TagModels");
            DropTable("dbo.ExpenseModels");
        }
    }
}
