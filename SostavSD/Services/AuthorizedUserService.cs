using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SostavSD.Entities;
using SostavSD.Interfaces;
using SostavSD.Models;
using System.Security.Claims;

namespace SostavSD.Services;

public class AuthorizedUserService : IAuthorizedUserService
{
    private readonly UserManager<UserSostav> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly IMapper _mapper;

    List<UserSostav> _users = new List<UserSostav>();

    public AuthorizedUserService(AuthenticationStateProvider authenticationStateProvider, UserManager<UserSostav> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager)
    {
        _authenticationStateProvider = authenticationStateProvider;
        _userManager = userManager;
        _mapper = mapper;
        _roleManager = roleManager;
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

    public async Task<List<UserSostavModel>> GetAllUsersAsync()
    {
        var user =  _userManager.Users.Select(x => new UserSostav

        {

            Id = x.Id,

            Surname = x.Surname,

            Email = x.Email,          

        });

        foreach (var item in user)

        {

            _users.Add(item);

        }

        return  _mapper.Map<List<UserSostavModel>>(_users);
    }

    public async Task<UserSostavModel> GetSingleUser(string userID)
    {
       var currentUser = _userManager.Users.FirstOrDefault(c => c.Id == userID);

        return _mapper.Map<UserSostavModel>(currentUser);
    }
}
