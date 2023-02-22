using Microsoft.AspNetCore.Components;
using SostavSD.Areas.Identity;

namespace SostavSD
{
    public partial class App
    {
        [Parameter] public TokenProvider InitialState { get; set; }
        protected override async Task<Task> OnInitializedAsync()
        {
            TokenProvider.XsrfToken = InitialState.XsrfToken;
            return base.OnInitializedAsync();
        }
    }
}
