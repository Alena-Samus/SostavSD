using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using SostavSD.Entities;
using SostavSD.Interfaces;
using SostavSD.Models;

namespace SostavSD.Pages.Adminitration
{
    partial class Administration
    {
        private IAuthorizedUserService _authorizedUserService;

        List<UserSostavModel> _users = new List<UserSostavModel>();

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

    }
}
