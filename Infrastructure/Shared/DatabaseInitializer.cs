using Domain.Entities;
using Infrastructure.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Constants;
using System.Security.Claims;

namespace Infrastructure.Shared
{
    public class DatabaseInitializer
    {
        public static void Initialize(ApplicationDbContext context, IServiceScope serviceScope)
        {
            _ = new DatabaseInitializer();
            Seed(context, serviceScope);
        }

        protected async static void Seed(ApplicationDbContext context, IServiceScope serviceScope)
        {
            var userManager = serviceScope.ServiceProvider.GetService<UserManager<User>>();
            var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<Role>>();
            var configuration = serviceScope.ServiceProvider.GetService<IConfiguration>();

            if (roleManager != null & userManager != null)
            {
                var adminRoleName = "Administrator";

                var existingRole = await roleManager.FindByNameAsync(adminRoleName);

                if (existingRole == null)
                {
                    var eventCreateClaim = new Claim(RoleClaims.Event_Create, "true");
                    var eventUpdateClaim = new Claim(RoleClaims.Event_Update, "true");
                    var eventDeleteClaim = new Claim(RoleClaims.Event_Delete, "true");

                    var newRole = new Role() { Name = adminRoleName };
                    await roleManager.CreateAsync(newRole);

                    await roleManager.AddClaimAsync(newRole, eventCreateClaim);
                    await roleManager.AddClaimAsync(newRole, eventUpdateClaim);
                    await roleManager.AddClaimAsync(newRole, eventDeleteClaim);
                }

                var userExists = userManager.GetUsersInRoleAsync(adminRoleName).Result;

                if (userExists.Count() == 0 && configuration != null)
                {
                    var adminCredentialsConfig = configuration.GetSection("AdminCredentialsConfig");

                    var user = new User
                    {
                        Email = adminCredentialsConfig["Username"],
                        UserName = adminCredentialsConfig["Username"],
                    };

                    _ = userManager.CreateAsync(user, adminCredentialsConfig["Password"]).Result;
                    _ = userManager.AddToRoleAsync(user, adminRoleName).Result;
                }
            }
        }
    }
}