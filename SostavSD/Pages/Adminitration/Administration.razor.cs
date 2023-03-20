using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using SostavSD.Entities;

namespace SostavSD.Pages.Adminitration
{
    partial class Administration
    {
        //List<string> Administrators = new List<string> { "one", "two", "three" };

        private UserManager<UserSostav> _userManager;

        List<UserSostav> _users = new List<UserSostav>();

        public Administration( UserManager<UserSostav> userManager)
        {
            _userManager = userManager;
        }

        protected override async Task OnInitializedAsync()
        {
            GetUsers();
        }

        public void GetUsers()

        {



            // Collection to hold users

            _users = new List<UserSostav>();

            // get users from _UserManager

            var user = _userManager.Users.Select(x => new UserSostav

            {

                Id = x.Id,

                UserName = x.UserName,

                Email = x.Email,

            });

            foreach (var item in user)

            {

                _users.Add(item);

            }

        }

    }
}
