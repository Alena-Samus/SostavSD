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


   private readonly List<ManagerUserModel> _users = new List<ManagerUserModel>();
   private readonly List<UserSostavModel> _executors = new List<UserSostavModel>();

    public AuthorizedUserService(AuthenticationStateProvider authenticationStateProvider, UserManager<UserSostav> userManager)
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

    public async Task<List<ManagerUserModel>> GetAllUsersAsync()
    {
        var users = _userManager.Users.Select(x => new ManagerUserModel

        {

            UserId = x.Id,
            UserSurname = x.Surname,
            UserEmail = x.Email,

        });

        foreach (var item in users)
        {
            var userRoles = await GetRoles(item.UserId);
            item.UserRoles = userRoles;

            _users.Add(item);
        }

        return _users;
    }
    public async Task <List<string>> GetRoles(string id)
    {
        var currentUser = await _userManager.FindByIdAsync(id);
        var userRoles = await _userManager.GetRolesAsync(currentUser);
        
        return userRoles.ToList();
    }
    public async Task<ManagerUserModel> GetSingleUser(string id)
    {
       var currentUser = await _userManager.FindByIdAsync(id);
        ManagerUserModel model = new ManagerUserModel
        {
            UserId = currentUser.Id,
            UserSurname = currentUser.Surname,
            UserEmail = currentUser.Email,
            UserRoles = new List<string>(),
        };

        return model;
    }

    public async Task ChangeUserRole(ManagerUserModel newRole)
    {
        string currentId = newRole.UserId;

        var currentUser = await _userManager.FindByIdAsync(currentId);

        await _userManager.AddToRolesAsync(currentUser, newRole.UserRoles);
    }

    public List<UserSostavModel> GetListUserSostavModel()
    {
        var user = _userManager.Users.Select(x => new UserSostavModel
        {
            Id = x.Id,
            Email = x.Email,

        });

        foreach (var item in user)
        {
            _executors.Add(item);
        }
        return _executors;
    }
    public List<UsersForList> GetListUserSostavModelByGroup(string group)
    {
        List<UsersForList> currentList = new List<UsersForList>();
        var user = _userManager.Users.Where(x => x.GroupName == group).Select(x => new UsersForList
        {
            IdUser = x.Id,
            SurnameUser = x.Surname,
        });
        foreach (var item in user)
        {
            currentList.Add(item);
        }
        return currentList;
    }
}
