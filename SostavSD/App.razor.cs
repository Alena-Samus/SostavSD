using Microsoft.AspNetCore.Components;
using SostavSD.Areas.Identity;

namespace SostavSD
{
    public partial class App : ComponentBase
    {
        [Parameter] public TokenProvider InitialState { get; set; }

        private TokenProvider TokenProvider { get; set; }

        public App(TokenProvider tokenProvider)
        {
            TokenProvider = tokenProvider;
        }

        protected override async Task OnInitializedAsync()
        {
            TokenProvider.XsrfToken = InitialState.XsrfToken;
        }
    }
}
