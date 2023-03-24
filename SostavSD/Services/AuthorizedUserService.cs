using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using SostavSD.Entities;
using SostavSD.Interfaces;
using SostavSD.Models;
using System.Security.Claims;

namespace SostavSD.Services;

public class AuthorizedUserService : IAuthorizedUserService
{
    private readonly UserManager<UserSostav> _userManager;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly IMapper _mapper;

    List<ManagerUserModel> _users = new List<ManagerUserModel>();
    List<ManagerUserModel> _resultUsers= new List<ManagerUserModel>();

    public AuthorizedUserService(AuthenticationStateProvider authenticationStateProvider, UserManager<UserSostav> userManager, IMapper mapper)
    {
        _authenticationStateProvider = authenticationStateProvider;
        _userManager = userManager;
        _mapper = mapper;
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

    public async Task<List<ManagerUserModel>> GetAllUsersAsync()
    {
        var user = _userManager.Users.Select(x => new ManagerUserModel

        {

            RegistredUserId = x.Id,
            RegistredUserSurname = x.Surname,
            RegistredUserEmail = x.Email,



        });

        foreach (var item in user)
        {
            item.RegistredUserRoles = new List<string>(await GetRoles(item.RegistredUserId));
            //item.RegistredUserRoles = await GetRoles(item.RegistredUserId);

            _users.Add(item);
        }

        return _users;
    }
    public async Task <List<string>> GetRoles(string id)
    {
        List<string> roles = new List<string>();
        var currentUser = _userManager.Users.FirstOrDefault(c => c.Id == id);
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity( await _userManager.GetClaimsAsync(currentUser)));
        var Claims = claimsPrincipal.Claims;
        foreach (var claim in Claims)
        {
            if(claim.Type == ClaimTypes.Role)
            {
                roles.Add(claim.Value);
            }
        }
        return roles;
    }
    public async Task<ManagerUserModel> GetSingleUser(string userID)
    {
       var currentUser = _userManager.Users.FirstOrDefault(c => c.Id == userID);
        ManagerUserModel model = new ManagerUserModel
        {
            RegistredUserId = currentUser.Id,
            RegistredUserSurname = currentUser.Surname,
            RegistredUserEmail = currentUser.Email,
            RegistredUserRoles = new List<string>(),
        };

        return model;
    }

    public async Task ChangeUserRole(ManagerUserModel newRole)
    {
        string currentId = newRole.RegistredUserId;

        var currentUser = await _userManager.FindByIdAsync(currentId);

        await _userManager.AddToRolesAsync(currentUser, newRole.RegistredUserRoles);
    }
}
