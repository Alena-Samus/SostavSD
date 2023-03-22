using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace SostavSD.Pages.Administration
{
    partial class EditUser : ComponentBase
    {
        [CascadingParameter] MudDialogInstance EditUserDialog { get; set; }

        void Submit()
        {
            EditUserDialog.Close(DialogResult.Ok(true));
        }
        
        void Cancel()
        {
            EditUserDialog.Cancel();
        }
    }
}
