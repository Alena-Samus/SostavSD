using Microsoft.AspNetCore.Identity;
using SostavSD.Areas.Identity.Constants;
using SostavSD.Entities;

namespace SostavSD.Data
{
    public class DBInitializerWithUsers
    {
        private const string DefaultPassword = "12345678";

        public async static Task InitializeUsers(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<SostavSDContext>();
            var _userManager = serviceProvider.GetService<UserManager<UserSostav>>();

            await AddRolesAsync(serviceProvider);
            await context.SaveChangesAsync();

            await AddUser("admin@test.com", Roles.AllRoles.ToArray(), _userManager, serviceProvider);
            await AddUser("readonly@test.com", new[] { Roles.ReadOnly }, _userManager, serviceProvider);
            await AddUser("chief@test.com", new[] { Roles.Chief, }, _userManager, serviceProvider);
            await AddUser("headofthegroup@test.com", new[] { Roles.HeadOfGroup, }, _userManager, serviceProvider);
            await AddUser("estimator@test.com", new[] { Roles.Estimator, }, _userManager, serviceProvider);
            await AddUser("calculator@test.com", new[] { Roles.Calculator, }, _userManager, serviceProvider);
            await AddUser("secretary@test.com", new[] { Roles.Secretary, }, _userManager, serviceProvider);
            await AddUser("inspector@test.com", new[] { Roles.Inspector, }, _userManager, serviceProvider);

            await context.SaveChangesAsync();
        }

        private async static Task AddUser(string email, string[] roles, UserManager<UserSostav> userManager, IServiceProvider serviceProvider)
        {
            var user = Activator.CreateInstance<UserSostav>();

            user.UserName = email;
            user.Email = email;
            var result = await userManager.CreateAsync(user, DefaultPassword);

            await AssignRoles(user.Email, roles, userManager);
        }

        private static async Task<IdentityResult> AssignRoles(string email, string[] roles, UserManager<UserSostav> userManager)
        {
            var user = await userManager.FindByEmailAsync(email);
            var result = await userManager.AddToRolesAsync(user, roles);

            return result;
        }

        private async static Task AddRolesAsync(IServiceProvider serviceProvider)
        {
            var roleMananger = serviceProvider.GetService<RoleManager<IdentityRole>>();

            var roles = Roles.AllRoles;

            var existingRoles = roleMananger.Roles.ToList();


            foreach (string role in roles)
            {
                if (!existingRoles.Any(r => r.Name == role))
                {
                    await roleMananger.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
