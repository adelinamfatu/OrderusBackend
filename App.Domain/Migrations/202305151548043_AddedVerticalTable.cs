namespace App.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedVerticalTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderExtendedProperties",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        Key = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .Index(t => t.OrderID);
            
            AddColumn("dbo.Comments", "OrderID", c => c.Int(nullable: false));
            AddColumn("dbo.Services", "UnitOfMeasurement", c => c.String());
            AddColumn("dbo.Orders", "Duration", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "IsConfirmed", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Comments", "OrderID");
            AddForeignKey("dbo.Comments", "OrderID", "dbo.Orders", "ID", cascadeDelete: true);
            DropColumn("dbo.Materials", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Materials", "Description", c => c.String());
            DropForeignKey("dbo.OrderExtendedProperties", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.Comments", "OrderID", "dbo.Orders");
            DropIndex("dbo.OrderExtendedProperties", new[] { "OrderID" });
            DropIndex("dbo.Comments", new[] { "OrderID" });
            DropColumn("dbo.Orders", "IsConfirmed");
            DropColumn("dbo.Orders", "Duration");
            DropColumn("dbo.Services", "UnitOfMeasurement");
            DropColumn("dbo.Comments", "OrderID");
            DropTable("dbo.OrderExtendedProperties");
        }
    }
}
