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
           GetUsers();
        }

        private async Task <List<ManagerUserModel>> GetUsers()
        {
            var users = await _authorizedUserService.GetAllUsersAsync();
            foreach (var user in users)
            {
                ManagerUserModel model = new ManagerUserModel
                {
                    RegistredUser = user,
                    RoleUser = ""
                };
                _usersForForm.Add(model);            }


            return _usersForForm;
        }

        private async Task EditUser(string userId)
        {
            //var _user = await _authorizedUserService.GetSingleUser(userId);

            DialogOptions closeOnEscapeKey = new DialogOptions() { CloseOnEscapeKey = true };

            _dialogService.Show<EditUser_Dialog>("Simple Dialog", closeOnEscapeKey);
        }


    }
}
