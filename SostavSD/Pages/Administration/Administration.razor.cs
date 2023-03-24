using MudBlazor;
using SostavSD.Interfaces;
using SostavSD.Models;



namespace SostavSD.Pages.Administration
{
    partial class Administration
    {
        private IAuthorizedUserService _authorizedUserService;
        private IDialogService _dialogService;


        List<ManagerUserModel> _usersForForm = new List<ManagerUserModel>();

        public Administration(IAuthorizedUserService authorizedUserService, IDialogService dialogService)
        {
            _authorizedUserService = authorizedUserService;
            _dialogService = dialogService;
        }

        protected override async Task OnInitializedAsync()
        {
            _usersForForm = await _authorizedUserService.GetAllUsersAsync();
        }

        private List<ManagerUserModel> GetUsers()
        {          
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
               GetUsers();
            }

        }


    }
}
