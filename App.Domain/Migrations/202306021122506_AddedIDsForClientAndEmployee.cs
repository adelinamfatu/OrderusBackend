namespace App.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIDsForClientAndEmployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "ID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Employees", "ID", c => c.Int(nullable: false, identity: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "ID");
            DropColumn("dbo.Clients", "ID");
        }
    }
}
