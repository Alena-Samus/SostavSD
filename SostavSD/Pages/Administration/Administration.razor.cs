using Microsoft.Extensions.Localization;
using MudBlazor;
using SostavSD.Interfaces;
using SostavSD.Models;



namespace SostavSD.Pages.Administration
{
    partial class Administration
    {
        private IAuthorizedUserService _authorizedUserService;
        private IDialogService _dialogService;
        private IStringLocalizer<Administration> _localizer;


        List<ManagerUserModel> _usersForForm = new List<ManagerUserModel>();

        public Administration(IAuthorizedUserService authorizedUserService, IDialogService dialogService, IStringLocalizer<Administration> localizer)
        {
            _authorizedUserService = authorizedUserService;
            _dialogService = dialogService;
            _localizer = localizer;
        }

        protected override async Task OnInitializedAsync()
        {
           await GetUsers();
        }

        private async Task<List<ManagerUserModel>> GetUsers()
        {
            _usersForForm = await _authorizedUserService.GetAllUsersAsync();
            return _usersForForm;
        }

        private async Task EditUser(string userId)
        {

            var parameters = new DialogParameters();
            var userToEdit = await _authorizedUserService.GetSingleUser(userId);
            parameters.Add("UserToEdit", userToEdit);
            var dialog = await _dialogService.Show<EditUserRole_Dialog>("update", parameters).Result;

            if (dialog != null)
            {
               await _authorizedUserService.ChangeUserRole(userToEdit);
               await GetUsers();
            }

        }


    }
}
