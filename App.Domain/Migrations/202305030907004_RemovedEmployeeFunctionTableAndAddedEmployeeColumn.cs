namespace App.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedEmployeeFunctionTableAndAddedEmployeeColumn : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Employees", "FunctionID", "dbo.EmployeeFunctions");
            DropForeignKey("dbo.FunctionServices", "FunctionID", "dbo.EmployeeFunctions");
            DropForeignKey("dbo.FunctionServices", "ServiceID", "dbo.Services");
            DropIndex("dbo.Employees", new[] { "FunctionID" });
            DropIndex("dbo.FunctionServices", new[] { "FunctionID" });
            DropIndex("dbo.FunctionServices", new[] { "ServiceID" });
            CreateTable(
                "dbo.EmployeeServices",
                c => new
                    {
                        EmployeeEmail = c.String(nullable: false, maxLength: 128),
                        ServiceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EmployeeEmail, t.ServiceID })
                .ForeignKey("dbo.Employees", t => t.EmployeeEmail, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceID, cascadeDelete: true)
                .Index(t => t.EmployeeEmail)
                .Index(t => t.ServiceID);
            
            AddColumn("dbo.Employees", "IsConfirmed", c => c.Boolean(nullable: false));
            DropColumn("dbo.Employees", "FunctionID");
            DropTable("dbo.EmployeeFunctions");
            DropTable("dbo.FunctionServices");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FunctionServices",
                c => new
                    {
                        FunctionID = c.Int(nullable: false),
                        ServiceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FunctionID, t.ServiceID });
            
            CreateTable(
                "dbo.EmployeeFunctions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Employees", "FunctionID", c => c.Int(nullable: false));
            DropForeignKey("dbo.EmployeeServices", "ServiceID", "dbo.Services");
            DropForeignKey("dbo.EmployeeServices", "EmployeeEmail", "dbo.Employees");
            DropIndex("dbo.EmployeeServices", new[] { "ServiceID" });
            DropIndex("dbo.EmployeeServices", new[] { "EmployeeEmail" });
            DropColumn("dbo.Employees", "IsConfirmed");
            DropTable("dbo.EmployeeServices");
            CreateIndex("dbo.FunctionServices", "ServiceID");
            CreateIndex("dbo.FunctionServices", "FunctionID");
            CreateIndex("dbo.Employees", "FunctionID");
            AddForeignKey("dbo.FunctionServices", "ServiceID", "dbo.Services", "ID", cascadeDelete: true);
            AddForeignKey("dbo.FunctionServices", "FunctionID", "dbo.EmployeeFunctions", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Employees", "FunctionID", "dbo.EmployeeFunctions", "ID", cascadeDelete: true);
        }
    }
}
