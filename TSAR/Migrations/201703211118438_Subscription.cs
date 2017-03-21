namespace TSAR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Subscription : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                   "dbo.Subscriptions",
                   c => new
                   {
                       Id = c.Int(nullable: false, identity: true),
                       Email = c.String(nullable: false),
                   })
               .PrimaryKey(t => t.Id);
        }
        
        public override void Down()
        {
            DropTable("dbo.Subscriptions");
        }
    }
}
