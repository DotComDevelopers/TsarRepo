namespace TSAR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class ManageTravel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.ManageTravels",
                    c => new
                    {
                        tcode = c.Int(nullable: false, identity: true),
                        travelCode = c.String(),
                        rate = c.Double(nullable: false),
                        distance = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.tcode);
        }


        public override void Down()
        {
            
            DropTable("dbo.ManageTravels");
        }
    }
}

