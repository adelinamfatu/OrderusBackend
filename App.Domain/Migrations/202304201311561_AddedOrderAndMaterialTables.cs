namespace App.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOrderAndMaterialTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeFunctions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                        Surname = c.String(),
                        Name = c.String(),
                        Password = c.String(),
                        Phone = c.String(),
                        CompanyID = c.Int(nullable: false),
                        Picture = c.String(),
                        FunctionID = c.Int(nullable: false),
                        EmployeeFunction_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Email)
                .ForeignKey("dbo.Companies", t => t.CompanyID, cascadeDelete: true)
                .ForeignKey("dbo.EmployeeFunctions", t => t.EmployeeFunction_ID)
                .Index(t => t.CompanyID)
                .Index(t => t.EmployeeFunction_ID);
            
            CreateTable(
                "dbo.FunctionServices",
                c => new
                    {
                        FunctionID = c.Int(nullable: false, identity: true),
                        ServiceID = c.Int(nullable: false),
                        EmployeeFunction_ID = c.Int(),
                    })
                .PrimaryKey(t => t.FunctionID)
                .ForeignKey("dbo.EmployeeFunctions", t => t.EmployeeFunction_ID)
                .ForeignKey("dbo.Services", t => t.ServiceID, cascadeDelete: true)
                .Index(t => t.ServiceID)
                .Index(t => t.EmployeeFunction_ID);
            
            CreateTable(
                "dbo.Materials",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Pret = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OrderMaterials",
                c => new
                    {
                        OrderID = c.Int(nullable: false),
                        MaterialID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderID, t.MaterialID })
                .ForeignKey("dbo.Materials", t => t.MaterialID, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.MaterialID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        RequireMaterial = c.Boolean(nullable: false),
                        ServiceID = c.Int(nullable: false),
                        ClientEmail = c.String(maxLength: 128),
                        EmployeeEmail = c.Int(nullable: false),
                        PaymentAmount = c.Single(nullable: false),
                        Employee_Email = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clients", t => t.ClientEmail)
                .ForeignKey("dbo.Employees", t => t.Employee_Email)
                .ForeignKey("dbo.Services", t => t.ServiceID, cascadeDelete: true)
                .Index(t => t.ServiceID)
                .Index(t => t.ClientEmail)
                .Index(t => t.Employee_Email);
            
            CreateTable(
                "dbo.StockMaterials",
                c => new
                    {
                        StockID = c.Int(nullable: false),
                        MaterialID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StockID, t.MaterialID })
                .ForeignKey("dbo.Materials", t => t.MaterialID, cascadeDelete: true)
                .ForeignKey("dbo.Stocks", t => t.StockID, cascadeDelete: true)
                .Index(t => t.StockID)
                .Index(t => t.MaterialID);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CompanyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Companies", t => t.CompanyID, cascadeDelete: true)
                .Index(t => t.CompanyID);
            
            AddColumn("dbo.Clients", "Picture", c => c.String());
            AddColumn("dbo.Comments", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockMaterials", "StockID", "dbo.Stocks");
            DropForeignKey("dbo.Stocks", "CompanyID", "dbo.Companies");
            DropForeignKey("dbo.StockMaterials", "MaterialID", "dbo.Materials");
            DropForeignKey("dbo.OrderMaterials", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "ServiceID", "dbo.Services");
            DropForeignKey("dbo.Orders", "Employee_Email", "dbo.Employees");
            DropForeignKey("dbo.Orders", "ClientEmail", "dbo.Clients");
            DropForeignKey("dbo.OrderMaterials", "MaterialID", "dbo.Materials");
            DropForeignKey("dbo.FunctionServices", "ServiceID", "dbo.Services");
            DropForeignKey("dbo.FunctionServices", "EmployeeFunction_ID", "dbo.EmployeeFunctions");
            DropForeignKey("dbo.Employees", "EmployeeFunction_ID", "dbo.EmployeeFunctions");
            DropForeignKey("dbo.Employees", "CompanyID", "dbo.Companies");
            DropIndex("dbo.Stocks", new[] { "CompanyID" });
            DropIndex("dbo.StockMaterials", new[] { "MaterialID" });
            DropIndex("dbo.StockMaterials", new[] { "StockID" });
            DropIndex("dbo.Orders", new[] { "Employee_Email" });
            DropIndex("dbo.Orders", new[] { "ClientEmail" });
            DropIndex("dbo.Orders", new[] { "ServiceID" });
            DropIndex("dbo.OrderMaterials", new[] { "MaterialID" });
            DropIndex("dbo.OrderMaterials", new[] { "OrderID" });
            DropIndex("dbo.FunctionServices", new[] { "EmployeeFunction_ID" });
            DropIndex("dbo.FunctionServices", new[] { "ServiceID" });
            DropIndex("dbo.Employees", new[] { "EmployeeFunction_ID" });
            DropIndex("dbo.Employees", new[] { "CompanyID" });
            DropColumn("dbo.Comments", "Date");
            DropColumn("dbo.Clients", "Picture");
            DropTable("dbo.Stocks");
            DropTable("dbo.StockMaterials");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderMaterials");
            DropTable("dbo.Materials");
            DropTable("dbo.FunctionServices");
            DropTable("dbo.Employees");
            DropTable("dbo.EmployeeFunctions");
        }
    }
}
