namespace App.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBooleanColumnForFinishedOrders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "IsFinished", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "IsFinished");
        }
    }
}
