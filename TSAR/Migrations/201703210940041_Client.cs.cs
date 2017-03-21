namespace TSAR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Clientcs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientName = c.String(nullable: false),
                        Branch = c.String(nullable: false),
                        ClientAddress = c.String(nullable: false),
                        ContactNumber = c.Long(nullable: false),
                        Email = c.String(nullable: false),
                        ClientType = c.String(nullable: false),
                        ProjectLeader = c.String(nullable: false),
                        TravelCode = c.Int(nullable: false),
                        RoleType = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Consultants",
                c => new
                    {
                        ConsultantNum = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        ContactNumber = c.Int(nullable: false),
                        ConsultantAddress = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        ConsultantType = c.String(nullable: false),
                        ComissionCode = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        RoleType = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ConsultantNum);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Consultants");
            DropTable("dbo.Clients");
        }
    }
}
