namespace App.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNullableAndForeignKeyCorrections : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Companies", "Site", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Companies", "Site");
        }
    }
}
