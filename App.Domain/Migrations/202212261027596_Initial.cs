namespace App.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Surname = c.String(),
                        Phone = c.String(),
                        Password = c.String(),
                        City = c.String(),
                        Street = c.String(),
                        StreetNumber = c.String(),
                        Building = c.String(),
                        Staircase = c.String(),
                        ApartmentNumber = c.Int(nullable: false),
                        Floor = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Email);
            
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
                "dbo.CompanyServiceOptions",
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
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ServiceCategories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.ServiceCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CompanyServiceOptions", "ServiceID", "dbo.Services");
            DropForeignKey("dbo.Services", "CategoryID", "dbo.ServiceCategories");
            DropForeignKey("dbo.CompanyServiceOptions", "CompanyID", "dbo.Companies");
            DropIndex("dbo.Services", new[] { "CategoryID" });
            DropIndex("dbo.CompanyServiceOptions", new[] { "CompanyID" });
            DropIndex("dbo.CompanyServiceOptions", new[] { "ServiceID" });
            DropTable("dbo.ServiceCategories");
            DropTable("dbo.Services");
            DropTable("dbo.CompanyServiceOptions");
            DropTable("dbo.Companies");
            DropTable("dbo.Clients");
        }
    }
}
