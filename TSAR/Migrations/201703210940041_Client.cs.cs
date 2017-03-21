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
            
           
            
        }
        
        public override void Down()
        {
          
            DropTable("dbo.Clients");
        }
    }
}
