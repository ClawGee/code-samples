namespace Sabio.Web.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Sabio.Web.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Sabio.Web.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

       
       

        protected override void Seed(Sabio.Web.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //


            CreateRole(context, "Admin");
            CreateRole(context, "SuperAdmin");
            CreateRole(context, "User");
            CreateRole(context, "Recruiter");
            CreateRole(context, "JobSeeker");


            if (!context.Users.Any(u => u.UserName == "c06"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "c06", Email = "c06_User@sabio.la", LockoutEnabled = false };

                manager.Create(user, "Sabiopass1!");
                manager.AddToRole(user.Id, "Admin");
                manager.AddToRole(user.Id, "SuperAdmin");


            }


            context.SaveChanges();
        }

        private static void CreateRole(ApplicationDbContext context, string roleName)
        {
            if (!context.Roles.Any(r => r.Name == roleName))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = roleName };

                manager.Create(role);
            }
        }
    }
}
