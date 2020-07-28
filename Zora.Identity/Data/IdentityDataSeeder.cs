using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using Zora.Identity.Data.Models;
using Zora.Shared;
using Zora.Shared.Services;

namespace Zora.Identity.Data
{

    public class IdentityDataSeeder : IDataSeeder
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public IdentityDataSeeder(
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public void SeedData()
        {
            if (this.roleManager.Roles.Any())
            {
                return;
            }

            Task
                .Run(async () =>
                {
                    var adminRole = new IdentityRole(Constants.AdministratorRoleName);

                    await this.roleManager.CreateAsync(adminRole);

                    var adminUser = new User
                    {
                        UserName = "hristina.palashka@gmail.com",
                        Email = "hristina.palashka@gmail.com",
                        SecurityStamp = "RandomSecurityStamp",
                        Name = "Hristina Palashka"
                    };

                    await userManager.CreateAsync(adminUser, "Parola123@");

                    await userManager.AddToRoleAsync(adminUser, Constants.AdministratorRoleName);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}
