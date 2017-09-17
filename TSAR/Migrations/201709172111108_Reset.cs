namespace TSAR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reset : DbMigration
    {
        public override void Up()
        {
      //CreateTable(
      //    "dbo.AccomodationFiles",
      //    c => new
      //        {
      //            accomodationfile_id = c.Guid(nullable: false, identity: true),
      //            file_name = c.String(),
      //            file_type = c.String(),
      //            file_bytes = c.Binary(nullable: false),
      //        })
      //    .PrimaryKey(t => t.accomodationfile_id);

      //CreateTable(
      //    "dbo.Budgets",
      //    c => new
      //        {
      //            BudgetCode = c.Int(nullable: false, identity: true),
      //            ProjectId = c.Int(nullable: false),
      //            Code = c.String(),
      //            Balance = c.Double(nullable: false),
      //        })
      //    .PrimaryKey(t => t.BudgetCode)
      //    .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
      //    .Index(t => t.ProjectId);

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
      //    .PrimaryKey(t => t.ProjectId)
      //    .ForeignKey("dbo.Clients", t => t.Id, cascadeDelete: true)
      //    .ForeignKey("dbo.Consultants", t => t.ConsultantNum, cascadeDelete: true)
      //    .Index(t => t.Id)
      //    .Index(t => t.ConsultantNum);

      //CreateTable(
      //    "dbo.Clients",
      //    c => new
      //        {
      //            Id = c.Int(nullable: false, identity: true),
      //            ClientName = c.String(nullable: false),
      //            ClientAddress = c.String(nullable: false),
      //            ClientAddress2 = c.String(),
      //            ContactNumber = c.Long(nullable: false),
      //            ContactNumber2 = c.String(),
      //            Email = c.String(nullable: false),
      //            Email2 = c.String(),
      //            Distance = c.String(),
      //            Distance2 = c.String(),
      //        })
      //    .PrimaryKey(t => t.Id);

      //CreateTable(
      //    "dbo.Consultants",
      //    c => new
      //        {
      //            ConsultantNum = c.Int(nullable: false, identity: true),
      //            FirstName = c.String(nullable: false),
      //            LastName = c.String(nullable: false),
      //            FullName = c.String(),
      //            Gender = c.String(nullable: false),
      //            ContactNumber = c.Int(nullable: false),
      //            ConsultantAddress = c.String(nullable: false),
      //            ConsultantType = c.String(nullable: false),
      //            CommissionId = c.Int(nullable: false),
      //            LeaveBalance = c.Int(nullable: false),
      //            AnnualLeaveBalance = c.Int(nullable: false),
      //            MaternityLeaveBalance = c.Int(nullable: false),
      //            SickLeaveBalance = c.Int(nullable: false),
      //            FamilyResponsibilityBalance = c.Int(nullable: false),
      //            PaternityLeaveBalance = c.Int(nullable: false),
      //            ConsultantUserName = c.String(nullable: false),
      //            Email = c.String(nullable: false),
      //            Password = c.String(nullable: false, maxLength: 100),
      //        })
      //    .PrimaryKey(t => t.ConsultantNum)
      //    .ForeignKey("dbo.Commissions", t => t.CommissionId, cascadeDelete: true)
      //    .Index(t => t.CommissionId);

      //CreateTable(
      //    "dbo.Commissions",
      //    c => new
      //        {
      //            CommissionId = c.Int(nullable: false, identity: true),
      //            CommissionName = c.String(nullable: false),
      //        })
      //    .PrimaryKey(t => t.CommissionId);

      //CreateTable(
      //    "dbo.ClientPasswords",
      //    c => new
      //        {
      //            ClientPasswordId = c.Int(nullable: false, identity: true),
      //            id = c.Int(nullable: false),
      //            Operator = c.String(),
      //            OperatorPassword = c.String(nullable: false),
      //            CompanyPassword = c.String(nullable: false),
      //        })
      //    .PrimaryKey(t => t.ClientPasswordId)
      //    .ForeignKey("dbo.Clients", t => t.id, cascadeDelete: true)
      //    .Index(t => t.id);

      //CreateTable(
      //    "dbo.Events",
      //    c => new
      //        {
      //            id = c.Int(nullable: false, identity: true),
      //            text = c.String(),
      //            start_date = c.DateTime(nullable: false),
      //            end_date = c.DateTime(nullable: false),
      //            name = c.String(),
      //        })
      //    .PrimaryKey(t => t.id);

      //CreateTable(
      //    "dbo.Files",
      //    c => new
      //        {
      //            FileId = c.Int(nullable: false, identity: true),
      //            FileName = c.String(maxLength: 255),
      //            FileType = c.Int(nullable: false),
      //            Content = c.Binary(),
      //        })
      //    .PrimaryKey(t => t.FileId);

      //CreateTable(
      //    "dbo.Leaves",
      //    c => new
      //        {
      //            LeaveId = c.Int(nullable: false, identity: true),
      //            FirstName = c.String(),
      //            ApprovedBy = c.String(),
      //            IsConfirmed = c.Boolean(nullable: false),
      //            LeaveDecsription = c.String(nullable: false),
      //            AccumulatedLeave = c.Int(nullable: false),
      //            AllocatedLeave = c.Int(nullable: false),
      //            LeaveCount = c.Int(nullable: false),
      //            AnnualBalance = c.Int(nullable: false),
      //            MaternityBalance = c.Int(nullable: false),
      //            SickBalance = c.Int(nullable: false),
      //            FamilyResponsibilityBalance = c.Int(nullable: false),
      //            PaternityBalance = c.Int(nullable: false),
      //            LeaveDate = c.DateTime(nullable: false),
      //            ReturnDate = c.DateTime(nullable: false),
      //            LeaveTypeName = c.String(),
      //            ConsultantNum = c.Int(nullable: false),
      //        })
      //    .PrimaryKey(t => t.LeaveId)
      //    .ForeignKey("dbo.Consultants", t => t.ConsultantNum, cascadeDelete: true)
      //    .Index(t => t.ConsultantNum);

      //CreateTable(
      //    "dbo.Locations",
      //    c => new
      //        {
      //            LocationId = c.Int(nullable: false, identity: true),
      //            Latitude = c.Double(nullable: false),
      //            Longitude = c.Double(nullable: false),
      //            CheckinTime = c.DateTime(nullable: false),
      //            Id = c.Int(nullable: false),
      //            ConsultantNum = c.Int(nullable: false),
      //        })
      //    .PrimaryKey(t => t.LocationId)
      //    .ForeignKey("dbo.Clients", t => t.Id, cascadeDelete: true)
      //    .ForeignKey("dbo.Consultants", t => t.ConsultantNum, cascadeDelete: true)
      //    .Index(t => t.Id)
      //    .Index(t => t.ConsultantNum);

      //CreateTable(
      //    "dbo.Payrolls",
      //    c => new
      //        {
      //            PayrollId = c.Int(nullable: false, identity: true),
      //            Basic = c.Double(nullable: false),
      //            Comm = c.Double(nullable: false),
      //            totPay = c.Double(nullable: false),
      //            tax = c.Double(nullable: false),
      //            ConsultantNum = c.Int(nullable: false),
      //            total = c.Double(nullable: false),
      //            Timesheet_TimesheetId = c.Int(),
      //        })
      //    .PrimaryKey(t => t.PayrollId)
      //    .ForeignKey("dbo.Consultants", t => t.ConsultantNum, cascadeDelete: true)
      //    .ForeignKey("dbo.Timesheets", t => t.Timesheet_TimesheetId)
      //    .Index(t => t.ConsultantNum)
      //    .Index(t => t.Timesheet_TimesheetId);

      //CreateTable(
      //    "dbo.Timesheets",
      //    c => new
      //        {
      //            TimesheetId = c.Int(nullable: false, identity: true),
      //            CaptureDate = c.DateTime(nullable: false),
      //            StartTime = c.Time(nullable: false, precision: 7),
      //            EndTime = c.Time(nullable: false, precision: 7),
      //            ActivityDescription = c.String(nullable: false),
      //            Total = c.Double(nullable: false),
      //            Hours = c.Double(nullable: false),
      //            Id = c.Int(nullable: false),
      //            ConsultantNum = c.Int(nullable: false),
      //            TicketReference = c.String(),
      //            SignOff = c.Boolean(nullable: false),
      //            Filename = c.String(),
      //            Signature = c.Binary(),
      //            MClientAddress = c.String(),
      //            ProjectName = c.String(),
      //            Project_ProjectId = c.Int(),
      //            Travel_TravelId = c.Int(),
      //        })
      //    .PrimaryKey(t => t.TimesheetId)
      //    .ForeignKey("dbo.Clients", t => t.Id, cascadeDelete: true)
      //    .ForeignKey("dbo.Consultants", t => t.ConsultantNum, cascadeDelete: true)
      //    .ForeignKey("dbo.Projects", t => t.Project_ProjectId)
      //    .ForeignKey("dbo.Travels", t => t.Travel_TravelId)
      //    .Index(t => t.Id)
      //    .Index(t => t.ConsultantNum)
      //    .Index(t => t.Project_ProjectId)
      //    .Index(t => t.Travel_TravelId);

      //CreateTable(
      //    "dbo.Travels",
      //    c => new
      //        {
      //            TravelId = c.Int(nullable: false, identity: true),
      //            Id = c.Int(nullable: false),
      //            MClientAddress = c.String(),
      //            TravelRate = c.Double(nullable: false),
      //            Distance = c.String(),
      //            TravelFee = c.Double(nullable: false),
      //        })
      //    .PrimaryKey(t => t.TravelId)
      //    .ForeignKey("dbo.Clients", t => t.Id, cascadeDelete: true)
      //    .Index(t => t.Id);

      //CreateTable(
      //    "dbo.Products",
      //    c => new
      //        {
      //            ProductId = c.Int(nullable: false, identity: true),
      //            ProductName = c.String(),
      //            Price = c.Double(nullable: false),
      //            Name = c.String(),
      //            Email = c.String(),
      //        })
      //    .PrimaryKey(t => t.ProductId);

      //CreateTable(
      //    "dbo.Ratings",
      //    c => new
      //        {
      //            RatingId = c.Int(nullable: false, identity: true),
      //            ConsultantName = c.String(),
      //            Rate = c.String(nullable: false),
      //            Comment = c.String(nullable: false),
      //            ClientUsername = c.String(),
      //            Consultant_ConsultantNum = c.Int(),
      //        })
      //    .PrimaryKey(t => t.RatingId)
      //    .ForeignKey("dbo.Consultants", t => t.Consultant_ConsultantNum)
      //    .Index(t => t.Consultant_ConsultantNum);

      //CreateTable(
      //    "dbo.AspNetRoles",
      //    c => new
      //        {
      //            Id = c.String(nullable: false, maxLength: 128),
      //            Name = c.String(nullable: false, maxLength: 256),
      //        })
      //    .PrimaryKey(t => t.Id)
      //    .Index(t => t.Name, unique: true, name: "RoleNameIndex");

      //CreateTable(
      //    "dbo.AspNetUserRoles",
      //    c => new
      //        {
      //            UserId = c.String(nullable: false, maxLength: 128),
      //            RoleId = c.String(nullable: false, maxLength: 128),
      //        })
      //    .PrimaryKey(t => new { t.UserId, t.RoleId })
      //    .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
      //    .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
      //    .Index(t => t.UserId)
      //    .Index(t => t.RoleId);

      //CreateTable(
      //    "dbo.Subscriptions",
      //    c => new
      //        {
      //            Id = c.Int(nullable: false, identity: true),
      //            Email = c.String(nullable: false),
      //        })
      //    .PrimaryKey(t => t.Id);

      //CreateTable(
      //    "dbo.Tasks",
      //    c => new
      //        {
      //            TaskID = c.Int(nullable: false, identity: true),
      //            Description = c.String(),
      //            StartDate = c.DateTime(nullable: false),
      //            EndDate = c.DateTime(nullable: false),
      //            ConsultantNum = c.Int(nullable: false),
      //            Status = c.Int(nullable: false),
      //        })
      //    .PrimaryKey(t => t.TaskID)
      //    .ForeignKey("dbo.Consultants", t => t.ConsultantNum, cascadeDelete: true)
      //    .Index(t => t.ConsultantNum);

      //CreateTable(
      //    "dbo.Tickets",
      //    c => new
      //        {
      //            ID = c.Int(nullable: false, identity: true),
      //            ClientName = c.String(),
      //            Email = c.String(),
      //            FaultDescription = c.String(nullable: false),
      //            Priority = c.String(),
      //            Date = c.DateTime(nullable: false),
      //            Status = c.String(),
      //            Category = c.String(),
      //            ConsultantName = c.String(),
      //            ConsultantId = c.Int(nullable: false),
      //            TicketReference = c.String(),
      //        })
      //    .PrimaryKey(t => t.ID);

      //CreateTable(
      //    "dbo.AspNetUsers",
      //    c => new
      //        {
      //            Id = c.String(nullable: false, maxLength: 128),
      //            Email = c.String(maxLength: 256),
      //            EmailConfirmed = c.Boolean(nullable: false),
      //            PasswordHash = c.String(),
      //            SecurityStamp = c.String(),
      //            PhoneNumber = c.String(),
      //            PhoneNumberConfirmed = c.Boolean(nullable: false),
      //            TwoFactorEnabled = c.Boolean(nullable: false),
      //            LockoutEndDateUtc = c.DateTime(),
      //            LockoutEnabled = c.Boolean(nullable: false),
      //            AccessFailedCount = c.Int(nullable: false),
      //            UserName = c.String(nullable: false, maxLength: 256),
      //        })
      //    .PrimaryKey(t => t.Id)
      //    .Index(t => t.UserName, unique: true, name: "UserNameIndex");

      //CreateTable(
      //    "dbo.AspNetUserClaims",
      //    c => new
      //        {
      //            Id = c.Int(nullable: false, identity: true),
      //            UserId = c.String(nullable: false, maxLength: 128),
      //            ClaimType = c.String(),
      //            ClaimValue = c.String(),
      //        })
      //    .PrimaryKey(t => t.Id)
      //    .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
      //    .Index(t => t.UserId);

      //CreateTable(
      //    "dbo.AspNetUserLogins",
      //    c => new
      //        {
      //            LoginProvider = c.String(nullable: false, maxLength: 128),
      //            ProviderKey = c.String(nullable: false, maxLength: 128),
      //            UserId = c.String(nullable: false, maxLength: 128),
      //        })
      //    .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
      //    .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
      //    .Index(t => t.UserId);

      //CreateTable(
      //    "dbo.UserTables",
      //    c => new
      //        {
      //            Id = c.Int(nullable: false, identity: true),
      //            Username = c.String(),
      //            Name = c.String(),
      //            ContactNo = c.String(),
      //        })
      //    .PrimaryKey(t => t.Id);

    }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tasks", "ConsultantNum", "dbo.Consultants");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Ratings", "Consultant_ConsultantNum", "dbo.Consultants");
            DropForeignKey("dbo.Payrolls", "Timesheet_TimesheetId", "dbo.Timesheets");
            DropForeignKey("dbo.Timesheets", "Travel_TravelId", "dbo.Travels");
            DropForeignKey("dbo.Travels", "Id", "dbo.Clients");
            DropForeignKey("dbo.Timesheets", "Project_ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Timesheets", "ConsultantNum", "dbo.Consultants");
            DropForeignKey("dbo.Timesheets", "Id", "dbo.Clients");
            DropForeignKey("dbo.Payrolls", "ConsultantNum", "dbo.Consultants");
            DropForeignKey("dbo.Locations", "ConsultantNum", "dbo.Consultants");
            DropForeignKey("dbo.Locations", "Id", "dbo.Clients");
            DropForeignKey("dbo.Leaves", "ConsultantNum", "dbo.Consultants");
            DropForeignKey("dbo.ClientPasswords", "id", "dbo.Clients");
            DropForeignKey("dbo.Budgets", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "ConsultantNum", "dbo.Consultants");
            DropForeignKey("dbo.Consultants", "CommissionId", "dbo.Commissions");
            DropForeignKey("dbo.Projects", "Id", "dbo.Clients");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Tasks", new[] { "ConsultantNum" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Ratings", new[] { "Consultant_ConsultantNum" });
            DropIndex("dbo.Travels", new[] { "Id" });
            DropIndex("dbo.Timesheets", new[] { "Travel_TravelId" });
            DropIndex("dbo.Timesheets", new[] { "Project_ProjectId" });
            DropIndex("dbo.Timesheets", new[] { "ConsultantNum" });
            DropIndex("dbo.Timesheets", new[] { "Id" });
            DropIndex("dbo.Payrolls", new[] { "Timesheet_TimesheetId" });
            DropIndex("dbo.Payrolls", new[] { "ConsultantNum" });
            DropIndex("dbo.Locations", new[] { "ConsultantNum" });
            DropIndex("dbo.Locations", new[] { "Id" });
            DropIndex("dbo.Leaves", new[] { "ConsultantNum" });
            DropIndex("dbo.ClientPasswords", new[] { "id" });
            DropIndex("dbo.Consultants", new[] { "CommissionId" });
            DropIndex("dbo.Projects", new[] { "ConsultantNum" });
            DropIndex("dbo.Projects", new[] { "Id" });
            DropIndex("dbo.Budgets", new[] { "ProjectId" });
            DropTable("dbo.UserTables");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Tickets");
            DropTable("dbo.Tasks");
            DropTable("dbo.Subscriptions");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Ratings");
            DropTable("dbo.Products");
            DropTable("dbo.Travels");
            DropTable("dbo.Timesheets");
            DropTable("dbo.Payrolls");
            DropTable("dbo.Locations");
            DropTable("dbo.Leaves");
            DropTable("dbo.Files");
            DropTable("dbo.Events");
            DropTable("dbo.ClientPasswords");
            DropTable("dbo.Commissions");
            DropTable("dbo.Consultants");
            DropTable("dbo.Clients");
            DropTable("dbo.Projects");
            DropTable("dbo.Budgets");
            DropTable("dbo.AccomodationFiles");
        }
    }
}
