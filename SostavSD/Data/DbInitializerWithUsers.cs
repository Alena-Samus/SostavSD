using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SostavSD.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SostavSD.Data
{
    public class DBInitializerWithUsers
    {
        public async static Task InitializeUsers(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<SostavSDContext>();

            var roleMananger = serviceProvider.GetService<RoleManager<IdentityRole>>();

            string[] roles = new string[] { "Owner", "Administrator", "Manager", "Editor", "Buyer", "Business", "Seller", "Subscriber" };

            foreach (string role in roles)
            {
                if (!context.Roles.Any(r => r.Name == role))
                {
                    await roleMananger.CreateAsync(new IdentityRole(role));
                }
            }

            await context.SaveChangesAsync();

            var _userStore = serviceProvider.GetService<IUserStore<IdentityUser>>();
            var _emailStore = serviceProvider.GetService<IUserEmailStore<IdentityUser>>();
            var _userManager = serviceProvider.GetService<UserManager<IdentityUser>>();


            var user = Activator.CreateInstance<IdentityUser>();

            await _userStore.SetUserNameAsync(user, "Test@test.com", CancellationToken.None);
            user.Email = "Test@test.com";
            //await _emailStore.SetEmailAsync(user, "Test@test.com", CancellationToken.None);
            var result = await _userManager.CreateAsync(user, "12345678");


            await AssignRoles(serviceProvider, user.Email, roles);

            await context.SaveChangesAsync();
        }

        public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string[] roles)
        {
            var _userManager = services.GetService<UserManager<IdentityUser>>();
            var user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.AddToRolesAsync(user, roles);

            return result;
        }
    }
}
