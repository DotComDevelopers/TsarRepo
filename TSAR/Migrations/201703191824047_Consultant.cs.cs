namespace TSAR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Consultantcs : DbMigration
    {
        public override void Up()
        {
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
        }
    }
}
