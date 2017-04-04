namespace TSAR.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'ed39fef3-a8b1-4f1b-839c-0e24f2bf0822', N'dotcomdevelopers19@gmail.com', 1, N'AKIG+PWG//ExWEW7EtSh/Ck5bMVn4j8zcrQjzZNa3z8fePixiaKCmgkceut+dQQpRQ==', N'2f5fac72-f9e8-4f57-a699-b97131121e22', N'0797697898', 0, 0, NULL, 0, 0, N'dotcomdevelopers19@gmail.com')

            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'ff89ce07-fb21-4001-a25a-ca5c73043d00', N'Admin')
            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'ed39fef3-a8b1-4f1b-839c-0e24f2bf0822', N'ff89ce07-fb21-4001-a25a-ca5c73043d00')

");
        }
        
        public override void Down()
        {
        }
    }
}
