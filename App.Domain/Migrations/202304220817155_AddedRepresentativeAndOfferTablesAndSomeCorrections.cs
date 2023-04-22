namespace App.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRepresentativeAndOfferTablesAndSomeCorrections : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Representatives",
                c => new
                    {
                        Email = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Surname = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Email);
            
            CreateTable(
                "dbo.Offers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClientID = c.Int(nullable: false),
                        DiscountValue = c.Int(nullable: false),
                        DiscountPercentage = c.Int(nullable: false),
                        Client_Email = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clients", t => t.Client_Email)
                .Index(t => t.Client_Email);
            
            AddColumn("dbo.Companies", "RepresentativeEmail", c => c.String(maxLength: 128));
            AddColumn("dbo.EmployeeFunctions", "Title", c => c.String());
            CreateIndex("dbo.Companies", "RepresentativeEmail");
            AddForeignKey("dbo.Companies", "RepresentativeEmail", "dbo.Representatives", "Email");
            DropColumn("dbo.EmployeeFunctions", "Name");
            DropColumn("dbo.EmployeeFunctions", "Surname");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EmployeeFunctions", "Surname", c => c.String());
            AddColumn("dbo.EmployeeFunctions", "Name", c => c.String());
            DropForeignKey("dbo.Offers", "Client_Email", "dbo.Clients");
            DropForeignKey("dbo.Companies", "RepresentativeEmail", "dbo.Representatives");
            DropIndex("dbo.Offers", new[] { "Client_Email" });
            DropIndex("dbo.Companies", new[] { "RepresentativeEmail" });
            DropColumn("dbo.EmployeeFunctions", "Title");
            DropColumn("dbo.Companies", "RepresentativeEmail");
            DropTable("dbo.Offers");
            DropTable("dbo.Representatives");
        }
    }
}
