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
            //Create default User and assign role
            if (!(context.Users.Any(u => u.UserName == "Admin")))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var userToInsert = new ApplicationUser
                {
                    UserName = "Admin",
                    PhoneNumber = "0797697898",
                    //Id = "34594e90-6b49-4c14-a858-deb018c5ad29",
                    Email = "dotcomdevelopers19@gmail.com",
                    EmailConfirmed = true,
                };
                userManager.Create(userToInsert, "Green1!");
                userManager.AddToRole(userToInsert.Id, "Admin");
            }

            //Create default Travel
            context.ManageTravels.AddOrUpdate(
              t => t.TravelCode,
              new ManageTravel() { TravelCode = "DUR", distance = 10, rate = 2, TravelFee = 20 }
             //new ManageTravel() { TravelCode = "CPT",distance = 20, rate =2 , TravelFee =40 },
                 );
            //Create default Client
            context.Clients.AddOrUpdate(
                c => c.Id,
                new Client()
                {
                    Id = 1,
                    Branch = "Test",
                    ClientAddress = "Test",
                    ClientName = "Test",
                    ContactNumber = 0765054589,
                    Email = "test@test.com",
                    ProjectLeader = "Test",
                    TravelCode = "DUR"
                }
                );

            //Create default Client
            context.Consultants.AddOrUpdate(
                c =>c.ConsultantNum,
                new Consultant()
                {
                    ComissionCode = "Test",
                    ConsultantAddress = "Test",
                    ConsultantNum = 1,
                    ConsultantType = "Finance",
                    ContactNumber = 0112026584,
                    Email = "test@test.com",
                    FirstName = "Jonny",
                    LastName = "Bravo",
                    Password = "test",
                    RoleType = "Admin"
                }
                ); 
            //Create default Subscription
            context.Subscriptions.AddOrUpdate(
                c =>c.Id,
                new Subscription()
                {
                    Id = 1,
                    Email = "test@test.com"  
                }
                );
              
        }
    }
}