using Microsoft.AspNetCore.Components;
using MudBlazor;
using SostavSD.Areas.Identity.Constants;
using SostavSD.Models;

namespace SostavSD.Pages.Administration
{
    partial class EditUserRole_Dialog : ComponentBase
    {
        [CascadingParameter] MudDialogInstance EditUserDialog { get; set; }
        [Parameter] public ManagerUserModel UserToEdit { get; set; }
        [Parameter] public string newRole { get; set; }

        List<string> _rolesList = Roles.AllRoles;

        void Submit() 
        {
            
            UserToEdit.UserRoles.Add(newRole);
            EditUserDialog.Close(DialogResult.Ok(UserToEdit));            
        }
        void Cancel() => EditUserDialog.Cancel();
    }
}
