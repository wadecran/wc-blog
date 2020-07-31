namespace wc_Blog.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using wc_Blog.Helpers;
    using wc_Blog.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<wc_Blog.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(wc_Blog.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var roleManager = new RoleManager<IdentityRole>(
               new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "Moderator"))
            {
                roleManager.Create(new IdentityRole { Name = "Moderator" });
            }

            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "wadecran@gmail.com"))
                {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Wade",
                    LastName = "Cranford",
                    Email = "wadecran@gmail.com",
                    UserName = "wadecran@gmail.com",
                    DisplayName = "Purple_Onion",
                
                }, "OpenMe123");
                var userId = userManager.FindByEmail("wadecran@gmail.com").Id;
                userManager.AddToRoles(userId, "Admin");
            }

            if (!context.Users.Any(u => u.Email == "JasonTwichell@coderfoundry.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Jason",
                    LastName = "Twichell",
                    Email = "JasonTwichell@coderfoundry.com",
                    UserName = "JasonTwichell@coderfoundry.com",
                    DisplayName = "The Prof",


                }, "OpenMe123");
                var userId = userManager.FindByEmail("JasonTwichell@coderfoundry.com").Id;
                userManager.AddToRoles(userId, "Moderator");
            }

            if (!context.Users.Any(u => u.Email == "arussell@coderfoundry.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Drew",
                    LastName = "Russell",
                    Email = "arussell@coderfoundry.com",
                    UserName = "arussell@coderfoundry.com",
                    DisplayName = "Famous",


                }, "OpenMe123");
                var userId = userManager.FindByEmail("arussell@coderfoundry.com").Id;
                userManager.AddToRoles(userId, "Moderator");
            }

            for (var loop = 1; loop <= 30; loop++) {
                context.BlogPosts.AddOrUpdate(
                    b => b.Title,
                    new BlogPost
                    {
                        Title = $"Seeded Title {loop}",
                        Body = $"Seeded Body {loop}",
                        Abstract = $"Seeded Abstract {loop}",
                        Slug = StringUtilities.URLFriendly($"Seeded Title {loop}"),
                        Published = true,
                        Created = DateTime.Now,
                    });
            }
        }
    }
}
