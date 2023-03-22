using Microsoft.AspNetCore.Components;
using MudBlazor;
using SostavSD.Areas.Identity.Constants;
using SostavSD.Models;

namespace SostavSD.Pages.Administration
{
    partial class EditUserRole_Dialog
    {
        [CascadingParameter] MudDialogInstance EditUserDialog { get; set; }
        [Parameter] public UserSostavModel UserToEdit { get; set; }
        [Parameter] public string NewRole { get; set; }

        List<string> _rolesList = Roles.AllRoles;

        void Submit() => EditUserDialog.Close(DialogResult.Ok(NewRole));
        void Cancel() => EditUserDialog.Cancel();
    }
}
