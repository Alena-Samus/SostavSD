using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis;
using MudBlazor;
using SostavSD.Interfaces;
using SostavSD.Models;
using SostavSD.Pages.Contracts;
using SostavSD.Pages.Projects;
using SostavSD.Pages.ProjectSostav;
using SostavSD.Pages.ProjectSostav.ProjectCard;

namespace SostavSD.Services
{
	public class EditService : IEditService

	{
        [Inject] ISnackbar Snackbar { get; set; }

        private readonly IContractService _contractService;
		private readonly IDialogService _dialogService;
		private readonly IProjectService _projectService;
		private readonly IDrawingService _drawingService;
        private readonly ISubsectionService _subsectionService;

        public EditService(IContractService contractService, IDialogService dialogService, IProjectService projectService, 
			
			IDrawingService drawingService, ISubsectionService subsectionService)
		{
			_contractService = contractService;
			_dialogService = dialogService;
			_projectService = projectService;
			_drawingService = drawingService;
            _subsectionService = subsectionService;
		}

		private bool result = false;

		public async Task<bool> EditContractDialog(int contractId)
		{
			if (contractId > 0)
			{
				var parameters = new DialogParameters();
				var contractToEdit = await _contractService.GetSingleContract(contractId);
				parameters.Add("Contract", contractToEdit);
				var dialog = await _dialogService.Show<ContractAddNewAndEdit>("update", parameters).Result;
				if (dialog.Data != null)
				{
					await _contractService.EditContract(contractToEdit);
					result = true;
				}	

			}

			return result;
		}
        public async Task<bool> EditProjectDialogAsync(ProjectModel newProject)
        {
            var parameters = new DialogParameters();

            parameters.Add("Project", newProject);
            var dialog = await _dialogService.Show<EditProject>("Edit", parameters).Result;
            if (dialog.Data != null)
            {
				if (await _projectService.EditProjectAsync((ProjectModel)dialog.Data))
				{
                    result = true;

                }
            }

            return result;
        }

        public async Task<bool> EditDrawingDialogAsync(int drawingId)
        {
            if (drawingId > 0)
            {
                var parameters = new DialogParameters();
               var _drawingToEdit = await _drawingService.GetSingleDrawingById(drawingId);

                parameters.Add("Drawing", _drawingToEdit);
                var dialog = await _dialogService.Show<EditDrawingDialog>("update", parameters).Result;
                if (dialog.Data != null)
                {
                    await _drawingService.EditDrawingAsync(_drawingToEdit);
                    result = true;
                }

            }
            return result;
        }

        public async Task<bool> EditSubsectionDialogAsync(int subsectionId)
        {
            if (subsectionId > 0)
            {
                var parameters = new DialogParameters();
                var _subsectionToEdit = await _subsectionService.GetSubsectionById(subsectionId);

                parameters.Add("Subsection", _subsectionToEdit);
                var dialog = await _dialogService.Show<EditSubsectionDialog>("update", parameters).Result;
                if (dialog.Data != null)
                {
                    await _subsectionService.EditSubsectionAsync(_subsectionToEdit);
                    result = true;
                }

            }
            return result;
        }


    }
}
