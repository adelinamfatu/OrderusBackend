namespace App.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedExpirationDayColumnOnOfferTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Offers", "ExpirationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Offers", "ExpirationDate");
        }
    }
}
