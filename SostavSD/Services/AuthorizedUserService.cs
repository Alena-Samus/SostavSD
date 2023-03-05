using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using SostavSD.Interfaces;
using System.Security.Claims;

namespace SostavSD.Services;

public class AuthorizedUserService : IAuthorizedUserService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    
    public AuthorizedUserService(AuthenticationStateProvider authenticationStateProvider, UserManager<IdentityUser> userManager)
    {
        _authenticationStateProvider = authenticationStateProvider;
        _userManager = userManager;
    }

    public async Task<bool> IsCurrentUserInRole(string role)
    {
        var authState = await _authenticationStateProvider
         .GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity.IsAuthenticated)
        {
            var currentUser = await _userManager.GetUserAsync(user);
            return await _userManager.IsInRoleAsync(currentUser, role);
        }
        else
        {
            return false;
        }
    }

    public async Task<ClaimsPrincipal> GetCurrentUserAsync()
    {
        var authState = await _authenticationStateProvider
            .GetAuthenticationStateAsync();
        var user = authState.User;

        return user;
    }
}
