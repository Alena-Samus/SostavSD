using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using SostavSD.Areas.Identity.Constants;
using SostavSD.Interfaces;
using System.Security.Claims;

namespace SostavSD.Pages;

public partial class IdentityUsageSample : ComponentBase
{
    private string? authMessage;
    private string? surname;
    private string? textFromCode;
    private IEnumerable<Claim> claims = Enumerable.Empty<Claim>();


    private readonly IAuthorizedUserService _authorizedUserService;

    public IdentityUsageSample(IAuthorizedUserService authorizedUserService)
    {
        _authorizedUserService = authorizedUserService;
    }

    protected override async Task OnInitializedAsync()
    {
        await GetClaimsPrincipalData();
        var isUserHasReadOnly = await _authorizedUserService.IsCurrentUserInRole(Roles.ReadOnly);

        textFromCode = isUserHasReadOnly
            ? "User has ReadOnly role"
            : "User doesnt has ReadOnly role";
    }

    private async Task GetClaimsPrincipalData()
    {
        var user = await _authorizedUserService.GetCurrentUserAsync();

        if (user.Identity is not null && user.Identity.IsAuthenticated)
        {
            authMessage = $"{user.Identity.Name} is authenticated.";
            claims = user.Claims;
            surname = user.FindFirst(c => c.Type == ClaimTypes.Surname)?.Value;
        }
        else
        {
            authMessage = "The user is NOT authenticated.";
        }
    }
}
