using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using SostavSD.Classes.Validation;
using SostavSD.Models;

namespace SostavSD.Pages.Projects
{
	partial class EditProject
	{
		[CascadingParameter] MudDialogInstance EditCurrentProject { get; set; }

		[Parameter] public ProjectModel Project { get; set; }
		[Inject] IStringLocalizer<EditProject> Localizer { get; set; }

		private void Cancel()
		{
			EditCurrentProject.Cancel();
		}

		private void Submit()
		{			
			EditCurrentProject.Close(DialogResult.Ok(Project));		
		}
		private void Remove()
		{
			EditCurrentProject.Cancel();
		}

	}
}
