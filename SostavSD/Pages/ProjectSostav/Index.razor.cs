using Microsoft.AspNetCore.Components;

namespace SostavSD.Pages.ProjectSostav
{
	partial class Index

	{
        [Inject] public NavigationManager NavigationManager { get; set; }
        [Parameter] public int ProjectId {get;set;}

        private void Navigate()
        {
            try
            {
                NavigationManager.NavigateTo($"/projectcard/{ProjectId}");
            }
            catch (Exception ex)
            {
                //logger.Error(ex);
                throw;
            }

        }
    }
}
