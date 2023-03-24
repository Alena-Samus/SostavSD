using SostavSD.Models;
using System.Security.Claims;

namespace SostavSD.Interfaces
{
    public interface IAuthorizedUserService
    {
        /// <summary>
        /// Check if current user has role
        /// </summary>
        /// <param name="role"></param>
        /// <returns>True if current user has role</returns>
        Task<bool> IsCurrentUserInRole(string role);

        /// <summary>
        /// Gets the current authorized user
        /// </summary>
        /// <returns>User details</returns>
        Task<ClaimsPrincipal> GetCurrentUserAsync();

        Task <List<ManagerUserModel>> GetAllUsersAsync();
        Task <ManagerUserModel> GetSingleUser(string userID);
        Task ChangeUserRole(ManagerUserModel newRole);
    }
}
