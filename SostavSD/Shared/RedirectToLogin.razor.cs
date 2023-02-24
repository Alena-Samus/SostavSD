namespace SostavSD.Shared
{
    public partial class RedirectToLogin
    {
        protected override void OnInitialized()
        {
            Navigation.NavigateTo("Identity/Account/Login?returnUrl=" +
                Uri.EscapeDataString(Navigation.Uri), true);
        }
    }
}
