namespace TSAR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class project : DbMigration
    {
        public override void Up()
        {
          //CreateTable(
          //    "dbo.Projects",
          //    c => new
          //    {
          //      ProjectId = c.Int(nullable: false, identity: true),
          //      ProjectName = c.String(),
          //      Id = c.Int(nullable: false),
          //      ConsultantNum = c.Int(nullable: false),
          //      StartDate = c.DateTime(nullable: false),
          //      EndDate = c.DateTime(nullable: false),
          //    })
          //  .PrimaryKey(t => t.ProjectId)
          //  .ForeignKey("dbo.Clients", t => t.Id, cascadeDelete: true)
          //  .ForeignKey("dbo.Consultants", t => t.ConsultantNum, cascadeDelete: true)
          //  .Index(t => t.Id)
          //  .Index(t => t.ConsultantNum);
    }
        
        public override void Down()
        {
        }
    }
}
