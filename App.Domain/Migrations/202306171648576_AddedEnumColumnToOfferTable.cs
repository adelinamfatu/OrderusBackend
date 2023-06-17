namespace App.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEnumColumnToOfferTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Offers", "Discount", c => c.Int(nullable: false));
            AddColumn("dbo.Offers", "Type", c => c.Int(nullable: false));
            DropColumn("dbo.Offers", "DiscountValue");
            DropColumn("dbo.Offers", "DiscountPercentage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Offers", "DiscountPercentage", c => c.Int(nullable: false));
            AddColumn("dbo.Offers", "DiscountValue", c => c.Int(nullable: false));
            DropColumn("dbo.Offers", "Type");
            DropColumn("dbo.Offers", "Discount");
        }
    }
}
