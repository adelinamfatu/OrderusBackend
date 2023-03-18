namespace App.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCSOCorrectionAndAddedCommentsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Score = c.Int(nullable: false),
                        ClientEmail = c.String(maxLength: 128),
                        CompanyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clients", t => t.ClientEmail)
                .ForeignKey("dbo.Companies", t => t.CompanyID, cascadeDelete: true)
                .Index(t => t.ClientEmail)
                .Index(t => t.CompanyID);
            
            AddColumn("dbo.CompanyServiceOptions", "Price", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "CompanyID", "dbo.Companies");
            DropForeignKey("dbo.Comments", "ClientEmail", "dbo.Clients");
            DropIndex("dbo.Comments", new[] { "CompanyID" });
            DropIndex("dbo.Comments", new[] { "ClientEmail" });
            DropColumn("dbo.CompanyServiceOptions", "Price");
            DropTable("dbo.Comments");
        }
    }
}
