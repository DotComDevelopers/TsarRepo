namespace TSAR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Budgets",
                c => new
                    {
                        BudgetCode = c.Int(nullable: false, identity: true),
                        Id = c.Int(nullable: false),
                        Code = c.String(),
                        Balance = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.BudgetCode)
                .ForeignKey("dbo.Clients", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientName = c.String(nullable: false),
                        ClientAddress = c.String(nullable: false),
                        ContactNumber = c.Long(nullable: false),
                        Email = c.String(nullable: false),
                        ProjectLeader = c.String(nullable: false),
                        Rate = c.Double(nullable: false),
                        Distance = c.String(),
                        TravelCode = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ManageTravels", t => t.TravelCode, cascadeDelete: true)
                .Index(t => t.TravelCode);
            
            CreateTable(
                "dbo.ManageTravels",
                c => new
                    {
                        TravelCode = c.String(nullable: false, maxLength: 128),
                        Rate = c.Double(nullable: false),
                        Distance = c.String(nullable: false),
                        TravelFee = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.TravelCode);
            
            CreateTable(
                "dbo.Commissions",
                c => new
                    {
                        CommissionId = c.Int(nullable: false, identity: true),
                        CommissionName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CommissionId);
            
            CreateTable(
                "dbo.Consultants",
                c => new
                    {
                        ConsultantNum = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        FullName = c.String(),
                        ContactNumber = c.Int(nullable: false),
                        ConsultantAddress = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        ConfirmEmail = c.String(nullable: false),
                        ConsultantType = c.String(nullable: false),
                        CommissionId = c.Int(nullable: false),
                        Password = c.String(nullable: false),
                        ConfirmPassword = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.ConsultantNum)
                .ForeignKey("dbo.Commissions", t => t.CommissionId, cascadeDelete: true)
                .Index(t => t.CommissionId);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        text = c.String(),
                        start_date = c.DateTime(nullable: false),
                        end_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        FileType = c.Int(nullable: false),
                        Content = c.Binary(),
                    })
                .PrimaryKey(t => t.FileId);
            
            CreateTable(
                "dbo.Leaves",
                c => new
                    {
                        LeaveId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        ApprovedBy = c.String(nullable: false),
                        IsConfirmed = c.Boolean(nullable: false),
                        LeaveDecsription = c.String(nullable: false),
                        LeaveDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.LeaveId);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        RatingId = c.Int(nullable: false, identity: true),
                        ConsultantName = c.String(),
                        Rate = c.String(nullable: false),
                        Comment = c.String(nullable: false),
                        ClientUsername = c.String(),
                        Consultant_ConsultantNum = c.Int(),
                    })
                .PrimaryKey(t => t.RatingId)
                .ForeignKey("dbo.Consultants", t => t.Consultant_ConsultantNum)
                .Index(t => t.Consultant_ConsultantNum);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Subscriptions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClientName = c.String(),
                        Email = c.String(),
                        FaultDescription = c.String(nullable: false),
                        Priority = c.String(),
                        Date = c.DateTime(nullable: false),
                        Status = c.String(),
                        Category = c.String(),
                        ConsultantName = c.String(),
                        Clients_Id = c.Int(),
                        Consultants_ConsultantNum = c.Int(),
                        Timesheets_TimesheetId = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clients", t => t.Clients_Id)
                .ForeignKey("dbo.Consultants", t => t.Consultants_ConsultantNum)
                .ForeignKey("dbo.Timesheets", t => t.Timesheets_TimesheetId)
                .Index(t => t.Clients_Id)
                .Index(t => t.Consultants_ConsultantNum)
                .Index(t => t.Timesheets_TimesheetId);
            
            CreateTable(
                "dbo.Timesheets",
                c => new
                    {
                        TimesheetId = c.Int(nullable: false, identity: true),
                        CaptureDate = c.DateTime(nullable: false),
                        StartTime = c.Time(nullable: false, precision: 7),
                        EndTime = c.Time(nullable: false, precision: 7),
                        ActivityDescription = c.String(nullable: false),
                        Total = c.Double(nullable: false),
                        Hours = c.Double(nullable: false),
                        Id = c.Int(nullable: false),
                        ConsultantNum = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TimesheetId)
                .ForeignKey("dbo.Clients", t => t.Id, cascadeDelete: true)
                .ForeignKey("dbo.Consultants", t => t.ConsultantNum, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.ConsultantNum);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Name = c.String(),
                        ContactNo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tickets", "Timesheets_TimesheetId", "dbo.Timesheets");
            DropForeignKey("dbo.Timesheets", "ConsultantNum", "dbo.Consultants");
            DropForeignKey("dbo.Timesheets", "Id", "dbo.Clients");
            DropForeignKey("dbo.Tickets", "Consultants_ConsultantNum", "dbo.Consultants");
            DropForeignKey("dbo.Tickets", "Clients_Id", "dbo.Clients");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Ratings", "Consultant_ConsultantNum", "dbo.Consultants");
            DropForeignKey("dbo.Consultants", "CommissionId", "dbo.Commissions");
            DropForeignKey("dbo.Budgets", "Id", "dbo.Clients");
            DropForeignKey("dbo.Clients", "TravelCode", "dbo.ManageTravels");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Timesheets", new[] { "ConsultantNum" });
            DropIndex("dbo.Timesheets", new[] { "Id" });
            DropIndex("dbo.Tickets", new[] { "Timesheets_TimesheetId" });
            DropIndex("dbo.Tickets", new[] { "Consultants_ConsultantNum" });
            DropIndex("dbo.Tickets", new[] { "Clients_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Ratings", new[] { "Consultant_ConsultantNum" });
            DropIndex("dbo.Consultants", new[] { "CommissionId" });
            DropIndex("dbo.Clients", new[] { "TravelCode" });
            DropIndex("dbo.Budgets", new[] { "Id" });
            DropTable("dbo.UserTables");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Timesheets");
            DropTable("dbo.Tickets");
            DropTable("dbo.Subscriptions");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Ratings");
            DropTable("dbo.Leaves");
            DropTable("dbo.Files");
            DropTable("dbo.Events");
            DropTable("dbo.Consultants");
            DropTable("dbo.Commissions");
            DropTable("dbo.ManageTravels");
            DropTable("dbo.Clients");
            DropTable("dbo.Budgets");
        }
    }
}
