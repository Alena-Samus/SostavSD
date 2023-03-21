﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using SostavSD.Entities;
using SostavSD.Interfaces;
using SostavSD.Models;
using System.Security.Principal;
using TanvirArjel.Blazor.Extensions;
using static System.Net.WebRequestMethods;

namespace SostavSD.Pages.Adminitration
{
    partial class Administration
    {
        private IAuthorizedUserService _authorizedUserService;

        List<UserSostavModel> _users = new List<UserSostavModel>();
        UserSostavModel _user = new UserSostavModel();

        public Administration(IAuthorizedUserService authorizedUserService)
        {
            _authorizedUserService = authorizedUserService;
        }

        protected override async Task OnInitializedAsync()
        {
           GetUsers();
        }

        private async Task <List<UserSostavModel>> GetUsers()
        {
            return _users = await _authorizedUserService.GetAllUsersAsync();
        }

        private async Task EditUser(string userId)
        {
            _user =   await _authorizedUserService.GetSingleUser(userId);
        }


    }
}
