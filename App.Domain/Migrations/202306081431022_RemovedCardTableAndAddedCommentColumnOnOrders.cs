namespace App.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedCardTableAndAddedCommentColumnOnOrders : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cards", "ClientEmail", "dbo.Clients");
            DropIndex("dbo.Cards", new[] { "ClientEmail" });
            AddColumn("dbo.Orders", "Comment", c => c.String());
            DropTable("dbo.Cards");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        Number = c.Int(nullable: false, identity: true),
                        ExpirationDate = c.DateTime(nullable: false),
                        ClientEmail = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Number);
            
            DropColumn("dbo.Orders", "Comment");
            CreateIndex("dbo.Cards", "ClientEmail");
            AddForeignKey("dbo.Cards", "ClientEmail", "dbo.Clients", "Email");
        }
    }
}
