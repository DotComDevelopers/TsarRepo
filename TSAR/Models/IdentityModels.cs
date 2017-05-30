using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TSAR.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public DbSet<UserTable> UserTables { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<TSAR.Models.ManageTravel> ManageTravels { get; set; }

        public System.Data.Entity.DbSet<TSAR.Models.Client> Clients { get; set; }

        public System.Data.Entity.DbSet<TSAR.Models.Consultant> Consultants { get; set; }

        public System.Data.Entity.DbSet<TSAR.Models.Subscription> Subscriptions { get; set; }

        public System.Data.Entity.DbSet<TSAR.Models.Commission> Commissions { get; set; }

        public System.Data.Entity.DbSet<TSAR.Models.Budget> Budgets { get; set; }

        public System.Data.Entity.DbSet<TSAR.Models.File> Files { get; set; }
        public System.Data.Entity.DbSet<TSAR.Models.Timesheet> Timesheets { get; set; }
        public System.Data.Entity.DbSet<TSAR.Models.Ticket> Tickets { get; set; }

        public System.Data.Entity.DbSet<TSAR.Models.Leave> Leaves { get; set; }

        public System.Data.Entity.DbSet<TSAR.Models.Rating> Ratings { get; set; }

        public System.Data.Entity.DbSet<TSAR.Models.Event> Events { get; set; }

        public System.Data.Entity.DbSet<TSAR.Models.LeaveType> LeaveTypes { get; set; }

    }
}