using DvD_Api.Models;
using Microsoft.AspNetCore.Identity;

namespace DvD_Api
{
    public class SeedData
    {

        public async Task CreateSuperAdmin(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<RopeyUserDto>>();

            IdentityResult roleResult;

            var roleNames = new List<string>
    {
        UserRoles.ADMIN,
        UserRoles.ASSISTANT
    };

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    // Create the roles if they do not exist.

                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }


            var superUser = new RopeyUserDto
            {
                FirstName = "Mula",
                LastName = "Prasad",
                Gender = "Female",
                UserName = "Young_mula_baby",
                DateOfBirth = DateTime.Parse("1969/04/20"),
                Email = "mula@gmail.com",
            };

            string superPassword = "Mypassword1!";
            var _user = await UserManager.FindByEmailAsync("mula@gmail.com");

            if (_user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(superUser, superPassword);
                if (createPowerUser.Succeeded)
                {
                    await UserManager.AddToRoleAsync(superUser, UserRoles.ADMIN);

                }
            }
        }
    }
}
