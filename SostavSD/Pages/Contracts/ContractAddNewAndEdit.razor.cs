using Microsoft.AspNetCore.Components;
using MudBlazor;
using SostavSD.Models;

namespace SostavSD.Pages.Contracts
{
    public partial class ContractAddNewAndEdit : ComponentBase
    {
        [CascadingParameter] MudDialogInstance AddOrEditContract { get; set; }
        [Parameter] public ContractModel Contract { get; set; }

        private void Cancel()
        {
            AddOrEditContract.Cancel();
           
        }

        private void Submit()
        {
            AddOrEditContract.Close(DialogResult.Ok(Contract));
        }
    }
}
