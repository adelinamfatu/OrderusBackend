namespace App.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLastTablesAndForeignKeyModifications : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.StockMaterials", "MaterialID", "dbo.Materials");
            DropForeignKey("dbo.Stocks", "CompanyID", "dbo.Companies");
            DropForeignKey("dbo.StockMaterials", "StockID", "dbo.Stocks");
            DropIndex("dbo.StockMaterials", new[] { "StockID" });
            DropIndex("dbo.StockMaterials", new[] { "MaterialID" });
            DropIndex("dbo.Stocks", new[] { "CompanyID" });
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Number = c.Int(nullable: false, identity: true),
                        ExpirationDate = c.DateTime(nullable: false),
                        ClientEmail = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Number)
                .ForeignKey("dbo.Clients", t => t.ClientEmail)
                .Index(t => t.ClientEmail);
            
            CreateTable(
                "dbo.FunctionServices",
                c => new
                    {
                        FunctionID = c.Int(nullable: false),
                        ServiceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FunctionID, t.ServiceID })
                .ForeignKey("dbo.EmployeeFunctions", t => t.FunctionID, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceID, cascadeDelete: true)
                .Index(t => t.FunctionID)
                .Index(t => t.ServiceID);
            
            AddColumn("dbo.Services", "Icon", c => c.String());
            AddColumn("dbo.Materials", "Price", c => c.Single(nullable: false));
            AddColumn("dbo.Materials", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.Materials", "CompanyID", c => c.Int(nullable: false));
            CreateIndex("dbo.Materials", "CompanyID");
            AddForeignKey("dbo.Materials", "CompanyID", "dbo.Companies", "ID", cascadeDelete: true);
            DropColumn("dbo.CompanyServiceOptions", "Description");
            DropColumn("dbo.CompanyServiceOptions", "Icon");
            DropColumn("dbo.EmployeeFunctions", "Description");
            DropColumn("dbo.Materials", "Pret");
            DropTable("dbo.StockMaterials");
            DropTable("dbo.Stocks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CompanyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.StockMaterials",
                c => new
                    {
                        StockID = c.Int(nullable: false),
                        MaterialID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StockID, t.MaterialID });
            
            AddColumn("dbo.Materials", "Pret", c => c.Single(nullable: false));
            AddColumn("dbo.EmployeeFunctions", "Description", c => c.String());
            AddColumn("dbo.CompanyServiceOptions", "Icon", c => c.String());
            AddColumn("dbo.CompanyServiceOptions", "Description", c => c.String());
            DropForeignKey("dbo.Materials", "CompanyID", "dbo.Companies");
            DropForeignKey("dbo.FunctionServices", "ServiceID", "dbo.Services");
            DropForeignKey("dbo.FunctionServices", "FunctionID", "dbo.EmployeeFunctions");
            DropForeignKey("dbo.Cards", "ClientEmail", "dbo.Clients");
            DropIndex("dbo.Materials", new[] { "CompanyID" });
            DropIndex("dbo.FunctionServices", new[] { "ServiceID" });
            DropIndex("dbo.FunctionServices", new[] { "FunctionID" });
            DropIndex("dbo.Cards", new[] { "ClientEmail" });
            DropColumn("dbo.Materials", "CompanyID");
            DropColumn("dbo.Materials", "Quantity");
            DropColumn("dbo.Materials", "Price");
            DropColumn("dbo.Services", "Icon");
            DropTable("dbo.FunctionServices");
            DropTable("dbo.Cards");
            CreateIndex("dbo.Stocks", "CompanyID");
            CreateIndex("dbo.StockMaterials", "MaterialID");
            CreateIndex("dbo.StockMaterials", "StockID");
            AddForeignKey("dbo.StockMaterials", "StockID", "dbo.Stocks", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Stocks", "CompanyID", "dbo.Companies", "ID", cascadeDelete: true);
            AddForeignKey("dbo.StockMaterials", "MaterialID", "dbo.Materials", "ID", cascadeDelete: true);
        }
    }
}
