namespace TSAR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Projects : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Budgets", "Id", "dbo.Clients");
            DropForeignKey("dbo.Projects", "Id", "dbo.Clients");
            DropIndex("dbo.Budgets", new[] { "Id" });
            DropIndex("dbo.Projects", new[] { "Id" });                
     
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "Id", c => c.Int(nullable: false));
            AddColumn("dbo.Budgets", "Id", c => c.Int(nullable: false));
            DropForeignKey("dbo.Timesheets", "Project_ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Budgets", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "ConsultantNum", "dbo.Consultants");
            DropIndex("dbo.Timesheets", new[] { "Project_ProjectId" });
            DropIndex("dbo.Projects", new[] { "ConsultantNum" });
            DropIndex("dbo.Budgets", new[] { "ProjectId" });
            DropColumn("dbo.Timesheets", "Project_ProjectId");
            DropColumn("dbo.Timesheets", "ProjectName");
            DropColumn("dbo.Timesheets", "Signature");
            DropColumn("dbo.Timesheets", "Filename");
            DropColumn("dbo.Projects", "EndDate");
            DropColumn("dbo.Projects", "StartDate");
            DropColumn("dbo.Projects", "ConsultantNum");
            DropColumn("dbo.Projects", "ProjectTitle");
            DropColumn("dbo.Leaves", "PaternityBalance");
            DropColumn("dbo.Leaves", "FamilyResponsibilityBalance");
            DropColumn("dbo.Leaves", "SickBalance");
            DropColumn("dbo.Leaves", "MaternityBalance");
            DropColumn("dbo.Leaves", "AnnualBalance");
            DropColumn("dbo.Consultants", "PaternityLeaveBalance");
            DropColumn("dbo.Consultants", "FamilyResponsibilityBalance");
            DropColumn("dbo.Consultants", "SickLeaveBalance");
            DropColumn("dbo.Consultants", "MaternityLeaveBalance");
            DropColumn("dbo.Consultants", "AnnualLeaveBalance");
            DropColumn("dbo.Budgets", "ProjectId");
            DropTable("dbo.AccomodationFiles");
            CreateIndex("dbo.Projects", "Id");
            CreateIndex("dbo.Budgets", "Id");
            AddForeignKey("dbo.Projects", "Id", "dbo.Clients", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Budgets", "Id", "dbo.Clients", "Id", cascadeDelete: true);
        }
    }
}
