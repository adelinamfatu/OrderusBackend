namespace App.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        City = c.String(),
                        Street = c.String(),
                        StreetNumber = c.String(),
                        Building = c.String(),
                        Staircase = c.String(),
                        ApartmentNumber = c.Int(nullable: false),
                        Floor = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CompaniesServiceOptions",
                c => new
                    {
                        ServiceID = c.Int(nullable: false),
                        CompanyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ServiceID, t.CompanyID })
                .ForeignKey("dbo.Companies", t => t.CompanyID, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceID, cascadeDelete: true)
                .Index(t => t.ServiceID)
                .Index(t => t.CompanyID);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CompaniesServiceOptions", "ServiceID", "dbo.Services");
            DropForeignKey("dbo.CompaniesServiceOptions", "CompanyID", "dbo.Companies");
            DropIndex("dbo.CompaniesServiceOptions", new[] { "CompanyID" });
            DropIndex("dbo.CompaniesServiceOptions", new[] { "ServiceID" });
            DropTable("dbo.Services");
            DropTable("dbo.CompaniesServiceOptions");
            DropTable("dbo.Companies");
        }
    }
}
