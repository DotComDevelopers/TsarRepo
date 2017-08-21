namespace TSAR.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TSAR.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<TSAR.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
          AutomaticMigrationDataLossAllowed = true;

        }

        protected override void Seed(TSAR.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.


            //Create default role
            if (!(context.Roles.Any(r => r.Name == "Admin")))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };
                manager.Create(role);
            }

            if (!(context.Roles.Any(r => r.Name == "Consultant")))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Consultant" };
                manager.Create(role);
            }

            if (!(context.Roles.Any(r => r.Name == "Client")))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Client" };
                manager.Create(role);
            }
            if (!(context.Roles.Any(r => r.Name == "Manager")))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Manager" };
                manager.Create(role);
            }
            //Create default User and assign role
            if (!(context.Users.Any(u => u.UserName == "dotcomdevelopers19@gmail.com")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var userToInsert = new ApplicationUser
                {
                    UserName = "dotcomdevelopers19@gmail.com",
                    PhoneNumber = "0797697898",
                    //Id = "34594e90-6b49-4c14-a858-deb018c5ad29",
                    Email = "dotcomdevelopers19@gmail.com",
                    EmailConfirmed = true
                };
                userManager.Create(userToInsert, "Green1!");
                userManager.AddToRole(userToInsert.Id, "Admin");
            }
            //Create default User and assign role
            if (!(context.Users.Any(u => u.UserName == "Suren")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var userToInsert = new ApplicationUser
                {
                    UserName = "Suren",
                    PhoneNumber = "0225458987",
                    //Id = "34594e90-6b49-4c14-a858-deb018c5ad29",
                    Email = "suren@gmail.com",
                    EmailConfirmed = true
                };
                userManager.Create(userToInsert, "Green1!");
                userManager.AddToRole(userToInsert.Id, "Consultant");
            }

            //Create default User and assign role
            if (!(context.Users.Any(u => u.UserName == "Devashen")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var userToInsert = new ApplicationUser
                {
                    UserName = "Devashen",
                    PhoneNumber = "0744119110",
                    //Id = "34594e90-6b49-4c14-a858-deb018c5ad29",
                    Email = "devashen@gmail.com",
                    EmailConfirmed = true
                };
                userManager.Create(userToInsert, "Green1!");
                userManager.AddToRole(userToInsert.Id, "Consultant");
            }

            if (!(context.Users.Any(u => u.UserName == "Admin")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var userToInsert = new ApplicationUser
                {
                    UserName = "Admin",
                    PhoneNumber = "0785256452",
                    //Id = "34594e90-6b49-4c14-a858-deb018c5ad29",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true
                };
                userManager.Create(userToInsert, "Green1!");
                userManager.AddToRole(userToInsert.Id, "Admin");
            }

            if (!(context.Users.Any(u => u.UserName == "Everest")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var userToInsert = new ApplicationUser
                {
                    UserName = "Everest",
                    PhoneNumber = "0765054589",
                    //Id = "34594e90-6b49-4c14-a858-deb018c5ad29",
                    Email = "nash@everest.com",
                    EmailConfirmed = true,
                };
                userManager.Create(userToInsert, "Green1!");
                userManager.AddToRole(userToInsert.Id, "Client");
            }

            //Create default Travel
            context.Travels.AddOrUpdate(
              t => t.TravelId,
              new Travel() { TravelId = 1, Distance = "1 km", TravelRate = 2, TravelFee = 2, Id = 1, MClientAddress = " 15 Narbada Road " },
              new Travel() { TravelId = 2, Distance = "10 km", TravelRate = 2, TravelFee = 20, Id = 1, MClientAddress = " 16 Narbada Road " }

                 //new ManageTravel() { TravelCode = "CPT",distance = 20, rate =2 , TravelFee =40 },
                 );

            //Create default Client
            context.Clients.AddOrUpdate(
                c => c.Id,
                new Client()
                {
                    Id = 1,
                    ClientAddress = "15 Narbada Road",
                    ClientAddress2 = "16 Narbada Road",
                    ClientName = "Everest",
                    ContactNumber = 0765054589,
                    Email = "nash@everest.com",
                    ContactNumber2 = "076505498989",
                    Email2 = "nash@everest.com",
                }
          );

            //Create default Commission
            context.Commissions.AddOrUpdate(
              c => c.CommissionId,
              new Commission()
              {
                  CommissionId = 1,
                  CommissionName = "5%"
              }
            );
            //Create default Consultant
            context.Consultants.AddOrUpdate(
                c => c.ConsultantNum,
                new Consultant()
                {
                    ConsultantNum = 1,
                    CommissionId = 1,
                    ConsultantAddress = "143 Warrangal Road",
                    ConsultantType = "Finance",
                    Password = "Green1!",
              //ConfirmPassword = "Green1!",
                    ContactNumber = 0745693488,
                    Email = "nerisha@gmail.com",
              //ConfirmEmail = "jonny@gmail.com",
                    FirstName = "Nerisha",
                    LastName = "Lutchman",
                    FullName = "Nerisha Lutchman",
                    ConsultantUserName = "Consultant",
                    Gender = "Female",
                    LeaveBalance = 24,
                    AnnualLeaveBalance = 21,
                    MaternityLeaveBalance = 120,
                    SickLeaveBalance = 13,
                    FamilyResponsibilityBalance = 3
                }
                );
            //Create default Consultant
            context.Consultants.AddOrUpdate(
              c => c.ConsultantNum,
              new Consultant()
              {
                  ConsultantNum = 2,
                  CommissionId = 1,
                  ConsultantAddress = "12 Narbada Road",
                  ConsultantType = "Finance",
                  Password = "Green1!",
            //ConfirmPassword = "Green1!",
                  ContactNumber = 0225458987,
                  Email = "suren@gmail.com",
            //ConfirmEmail = "jonny@gmail.com",
                  FirstName = "Suren",
                  LastName = "Naidoo",
                  FullName = "Suren Naidoo",
                  ConsultantUserName = "Suren",
                  Gender = "Male",
                  LeaveBalance = 24,
                  AnnualLeaveBalance = 21,
                  PaternityLeaveBalance = 120,
                  SickLeaveBalance = 13,
                  FamilyResponsibilityBalance = 3
              }
            );
            //Create default Consultant
            context.Consultants.AddOrUpdate(
              c => c.ConsultantNum,
              new Consultant()
              {
                  ConsultantNum = 3,
                  CommissionId = 1,
                  ConsultantAddress = "72 Aurora Road",
                  ConsultantType = "Finance",
                  Password = "Green1!",
            //ConfirmPassword = "Green1!",
                  ContactNumber = 0744119110,
                  Email = "devashen@gmail.com",
            //ConfirmEmail = "jonny@gmail.com",
                  FirstName = "Devashen",
                  LastName = "Govender",
                  FullName = "Devashen Govender",
                  ConsultantUserName = "Devashen",
                  Gender = "Male",
                  LeaveBalance = 24,
                  AnnualLeaveBalance = 21,
                  PaternityLeaveBalance = 120,
                  SickLeaveBalance = 13,
                  FamilyResponsibilityBalance = 3
              }
            );
            if (!(context.Users.Any(u => u.UserName == "Consultant")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var userToInsert = new ApplicationUser
                {
                    UserName = "Consultant",
                    PhoneNumber = "0745693488",
                    //Id = "34594e90-6b49-4c14-a858-deb018c5ad29",
                    Email = "nerisha@gmail.com",
                    EmailConfirmed = true,
                    //ConsultantNum = 1,
                };
                userManager.Create(userToInsert, "Green1!");
                userManager.AddToRole(userToInsert.Id, "Consultant");
            }
            //Create default Subscription
            context.Subscriptions.AddOrUpdate(
                c => c.Id,
                new Subscription()
                {
                    Id = 1,
                    Email = "john@gmail.com"
                }
                );


            //context.Budgets.AddOrUpdate(
            //    c => c.BudgetCode,
            //    new Budget()
            //    {
            //        BudgetCode = 1,
            //         ProjectId= "",
            //        Code = "Wk-23y734",
            //        Balance = 100000
            //    }
            //    );

            context.Timesheets.AddOrUpdate(
                c => c.TimesheetId,
                new Timesheet()
                {
                    TimesheetId = 1,
                    ActivityDescription = "Site Audit",
                    CaptureDate = System.DateTime.Now,
                    Id = 1,
                    ConsultantNum = 1,
                    StartTime = System.TimeSpan.FromHours(12),
                    EndTime = System.TimeSpan.FromHours(13),
                    Hours = 1,
                    Total = 700

                }
            );

            context.Timesheets.AddOrUpdate(
              c => c.TimesheetId,
              new Timesheet()
              {
                  TimesheetId = 2,
                  ActivityDescription = "Site Visit",
                  CaptureDate = System.DateTime.Now,
                  Id = 1,
                  ConsultantNum = 2,
                  StartTime = System.TimeSpan.FromHours(12),
                  EndTime = System.TimeSpan.FromHours(13),
                  Hours = 1,
                  Total = 700

              }
            );



            context.LeaveTypes.AddOrUpdate(
                      c => c.LeaveTypeId,
                      new LeaveType()
                      {
                          LeaveTypeId = 1,
                          LeaveTypeName = "Annual"

                      });

            context.LeaveTypes.AddOrUpdate(
                c => c.LeaveTypeId,
                new LeaveType()
                {
                    LeaveTypeId = 2,
                    LeaveTypeName = "Maternity"
                });

            context.LeaveTypes.AddOrUpdate(
                c => c.LeaveTypeId,
                new LeaveType()
                {
                    LeaveTypeId = 3,
                    LeaveTypeName = "Sick"
                });

            context.LeaveTypes.AddOrUpdate(
                c => c.LeaveTypeId,
                new LeaveType()
                {
                    LeaveTypeId = 4,
                    LeaveTypeName = "Family Resonsibilty"
                });

            context.LeaveTypes.AddOrUpdate(
                c => c.LeaveTypeId,
                new LeaveType()
                {
                    LeaveTypeId = 5,
                    LeaveTypeName = "Compassionate"
                });

            context.LeaveTypes.AddOrUpdate(
                c => c.LeaveTypeId,
                new LeaveType()
                {
                    LeaveTypeId = 6,
                    LeaveTypeName = "Paternity"
                });

            context.LeaveTypes.AddOrUpdate(
                c => c.LeaveTypeId,
                new LeaveType()
                {
                    LeaveTypeId = 7,
                    LeaveTypeName = "Disability"
                });

            context.LeaveTypes.AddOrUpdate(
                c => c.LeaveTypeId,
                new LeaveType()
                {
                    LeaveTypeId = 8,
                    LeaveTypeName = "Study"
                });

            context.LeaveTypes.AddOrUpdate(
                c => c.LeaveTypeId,
                new LeaveType()
                {
                    LeaveTypeId = 9,
                    LeaveTypeName = "Religious"
                });

            context.Leaves.AddOrUpdate(
       c => c.LeaveId,
       new Leave()
       {
           LeaveId = 1,
           FirstName = "Suren",
           ApprovedBy = "Admin",
           IsConfirmed = true,
           LeaveDecsription = "Sick Leave",
           LeaveDate = DateTime.Now,
           ReturnDate = DateTime.Now,
           ConsultantNum = 2,
           //LeaveTypeId = 1,
           AccumulatedLeave = 24,
           LeaveCount = 1,
           AllocatedLeave = 24,
           LeaveTypeName = "Sick",
           SickBalance = 13,
           AnnualBalance = 21,
           FamilyResponsibilityBalance = 3

       });

            context.Tickets.AddOrUpdate(
                t => t.ID,
                new Ticket()
                {
                    ID = 1,
                    ClientName = "Everest",
                    Email = "nash@everest.com",
                    Category = "Software",
                    Priority = "High",
                    FaultDescription = "My computer keeps on crashing and it also displays a blue screen after it crashes, on the screen it says problem cause by SPCMDCON.SYS",
                    Status = "Open Ticket",
                    Date = Convert.ToDateTime("2017-06-11 11:07 PM"),
                    TicketReference = "201706112nash",
                    ConsultantName = null,
                    ConsultantId = Convert.ToInt32(null)
                });


        }
    }
}