namespace App.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCorrectionsToLastMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FunctionServices", "EmployeeFunction_ID", "dbo.EmployeeFunctions");
            DropForeignKey("dbo.FunctionServices", "ServiceID", "dbo.Services");
            DropForeignKey("dbo.Employees", "EmployeeFunction_ID", "dbo.EmployeeFunctions");
            DropIndex("dbo.Employees", new[] { "EmployeeFunction_ID" });
            DropIndex("dbo.FunctionServices", new[] { "ServiceID" });
            DropIndex("dbo.FunctionServices", new[] { "EmployeeFunction_ID" });
            DropIndex("dbo.Orders", new[] { "Employee_Email" });
            DropColumn("dbo.Employees", "FunctionID");
            DropColumn("dbo.Orders", "EmployeeEmail");
            RenameColumn(table: "dbo.Employees", name: "EmployeeFunction_ID", newName: "FunctionID");
            RenameColumn(table: "dbo.Offers", name: "Client_Email", newName: "ClientEmail");
            RenameColumn(table: "dbo.Orders", name: "Employee_Email", newName: "EmployeeEmail");
            RenameIndex(table: "dbo.Offers", name: "IX_Client_Email", newName: "IX_ClientEmail");
            AlterColumn("dbo.Employees", "FunctionID", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "EmployeeEmail", c => c.String(maxLength: 128));
            CreateIndex("dbo.Employees", "FunctionID");
            CreateIndex("dbo.Orders", "EmployeeEmail");
            AddForeignKey("dbo.Employees", "FunctionID", "dbo.EmployeeFunctions", "ID", cascadeDelete: true);
            DropColumn("dbo.Offers", "ClientID");
            DropTable("dbo.FunctionServices");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FunctionServices",
                c => new
                    {
                        FunctionID = c.Int(nullable: false, identity: true),
                        ServiceID = c.Int(nullable: false),
                        EmployeeFunction_ID = c.Int(),
                    })
                .PrimaryKey(t => t.FunctionID);
            
            AddColumn("dbo.Offers", "ClientID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Employees", "FunctionID", "dbo.EmployeeFunctions");
            DropIndex("dbo.Orders", new[] { "EmployeeEmail" });
            DropIndex("dbo.Employees", new[] { "FunctionID" });
            AlterColumn("dbo.Orders", "EmployeeEmail", c => c.Int(nullable: false));
            AlterColumn("dbo.Employees", "FunctionID", c => c.Int());
            RenameIndex(table: "dbo.Offers", name: "IX_ClientEmail", newName: "IX_Client_Email");
            RenameColumn(table: "dbo.Orders", name: "EmployeeEmail", newName: "Employee_Email");
            RenameColumn(table: "dbo.Offers", name: "ClientEmail", newName: "Client_Email");
            RenameColumn(table: "dbo.Employees", name: "FunctionID", newName: "EmployeeFunction_ID");
            AddColumn("dbo.Orders", "EmployeeEmail", c => c.Int(nullable: false));
            AddColumn("dbo.Employees", "FunctionID", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "Employee_Email");
            CreateIndex("dbo.FunctionServices", "EmployeeFunction_ID");
            CreateIndex("dbo.FunctionServices", "ServiceID");
            CreateIndex("dbo.Employees", "EmployeeFunction_ID");
            AddForeignKey("dbo.Employees", "EmployeeFunction_ID", "dbo.EmployeeFunctions", "ID");
            AddForeignKey("dbo.FunctionServices", "ServiceID", "dbo.Services", "ID", cascadeDelete: true);
            AddForeignKey("dbo.FunctionServices", "EmployeeFunction_ID", "dbo.EmployeeFunctions", "ID");
        }
    }
}
