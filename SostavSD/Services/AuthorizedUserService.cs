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
            RegistredUserRoles = new List<string>(),

        });

        foreach (var item in user)

        {

            _users.Add(item);

        }
        //foreach (var elem in _users)

        //{

        //    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(await _userManager.GetClaimsAsync(_userManager.get))); ;

        //    RolesUser = claimsPrincipal.FindFirst(c => c.Type == ClaimTypes.Email)?.Value.ToString(),
        //};
        //_resultUsers.Add(model); 
        return _users;
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
        Console.WriteLine("*****");

        var currentUser = await _userManager.FindByIdAsync(currentId);

        Console.WriteLine($"currentUser.Id: {currentUser.Id}");
        Console.WriteLine($"currentUser.Surname: {currentUser.Surname}");
        Console.WriteLine($"currentUser.Email: {currentUser.Email}");
        Console.WriteLine($"RegistredUserNewRole: {newRole.RegistredUserRoles}");

        await _userManager.AddToRolesAsync(currentUser, newRole.RegistredUserRoles);
    }
}
