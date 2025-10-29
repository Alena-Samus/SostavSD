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
	public class EntityManagementService : IEntityManagementService

	{
        [Inject] ISnackbar Snackbar { get; set; }

        private readonly IContractService _contractService;
		private readonly IDialogService _dialogService;
		private readonly IBuildingViewService _buildingViewService;
		private readonly IBuildingZoneService _buildingZoneService;
		private readonly IDesignStageService _designStageService;
		private readonly IProjectService _projectService;
		private readonly IContractForTableService _contractForTableService;
		private readonly IStatusService _statusService;
		private readonly ISourceOfFinancingService _sourceOfFinancingService;
		private readonly IDrawingService _drawingService;
        private readonly IDeppartService _deppartService;
        private readonly ISubsectionService _subsectionService;

        public EntityManagementService(IContractService contractService, IDialogService dialogService, IBuildingViewService buildingViewService, 
			IDesignStageService designStageService, IProjectService projectService, IContractForTableService contractForTableService, 
			IStatusService statusService, IBuildingZoneService buildingZoneService, ISourceOfFinancingService sourceOfFinancingService, 
			IDrawingService drawingService, IDeppartService deppartService, ISubsectionService subsectionService)
		{
			_contractService = contractService;
			_dialogService = dialogService;
			_buildingViewService = buildingViewService;
			_designStageService = designStageService;
			_projectService = projectService;
			_contractForTableService = contractForTableService;
			_statusService = statusService;
			_buildingZoneService = buildingZoneService;
			_sourceOfFinancingService = sourceOfFinancingService;
			_drawingService = drawingService;
            _deppartService = deppartService;
            _subsectionService = subsectionService;
		}

		private bool result = false;

		public async Task<bool> EditContractDialog(int contractId)
		{
			if (contractId > 0)
			{
				var parameters = new DialogParameters();
				var contractToEdit = await GetSingleContract(contractId);
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
               var _drawingToEdit = await GetSingleDrawingById(drawingId);

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
                var _subsectionToEdit = await GetSubsectionById(subsectionId);

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


        public async Task<List<ContractForTableModel>> GetContractsAsync()
		{
			return await _contractForTableService.GetContractsAsync();
		}

		public List<BuildingViewModel> GetAllBuildingView()
		{
			return _buildingViewService.GetAllBuildingView();
		}

		public async Task<List<DesignStageModel>> GetAllDesignStageAsync()
		{
			return await _designStageService.GetAllDesignStageAsync();
		}

		public async Task<bool> AddProjectAsync(ProjectModel newProject)
		{
			return await _projectService.AddProjectAsync(newProject);
		}

		public async Task<bool> DeleteProjectAsync(int id)
		{
			return await _projectService.DeleteProjectAsync(id);
		}
	


		public async Task<ProjectModel> GetProjectByIdAsync(int id)
		{
			return await _projectService.GetProjectByIdAsync(id);
		}

		public async Task<List<StatusModel>> GetAllStatusAsync()
		{
			return await _statusService.GetAllStatusAsync();
		}

        public async Task<List<BuildingZoneModel>> GetBuildingZoneModelsAsync()
        {
			return await _buildingZoneService.GetBuildingZoneModelsAsync();
        }

        public async Task<List<ContractModel>> GetAllContract()
        {
            return await _contractService.GetAllContract();
        }

        public Task DeleteContract(int contractId)
        {
            throw new NotImplementedException();
        }

        public Task AddContract(ContractModel newContract)
        {
            throw new NotImplementedException();
        }

        public Task<List<ContractModel>> GetCurrentUserContracts(string userId)
        {
            throw new NotImplementedException();
        }

        public Task EditContract(ContractModel currentContract)
        {
            throw new NotImplementedException();
        }

        public Task<ContractForTableModel> GetContractByIdAsync(int contractId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProjectModel>> GetProjectsAsync()
        {
           return await _projectService.GetProjectsAsync();
        }

        public async Task<ContractModel> GetSingleContract(int contractId)
        {
            return await _contractService.GetSingleContract(contractId);
        }

        public BuildingZoneModel GetBuildingZoneById(int zoneId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SourceOfFinacingModel>> GetSourcesOfFinancingModelAsync()
        {
            return await _sourceOfFinancingService.GetSourcesOfFinancingModelAsync();
        }

        public bool CheckBuildingNumber(string buildingNumber)
        {
            return _projectService.CheckBuildingNumber(buildingNumber);
        }

        public async Task<List<DrawingModel>> GetDrawingModelsAsync()
        {
           return await _drawingService.GetDrawingModelsAsync();
        }
        public async Task<List<DrawingModel>> GetDrawingModelByProjectIdAsync(int id)
		{
            return await _drawingService.GetDrawingModelByProjectIdAsync(id);
        }

        public async Task<bool> AddDrawingsAsync(List<DrawingModel> drawingsList)
        {
            return await _drawingService.AddDrawingsAsync(drawingsList);
        }

        public async Task<bool> RemoveDrawingsAsync(int id)
        {
            return await _drawingService.RemoveDrawingsAsync(id);
        }

        public async Task<List<DeppartModel>> GetDeppartsAsync()
        {
            return await _deppartService.GetDeppartsAsync();
        }

        public async Task<List<DeppartModel>> GetDeppartsByGroupsAsync(List<string> groups)
        {
            return await _deppartService.GetDeppartsByGroupsAsync(groups);
        }

        public Task<bool> EditDrawingAsync(DrawingModel currentDrawing)
        {
            throw new NotImplementedException();
        }

        public async Task<DrawingModel> GetSingleDrawingById(int drawingId)
        {
           return await _drawingService.GetSingleDrawingById(drawingId);
        }

        public async Task<List<SubsectionModel>> GetSubsectionByProjectIdAsync(int projectId)
        {
           return await _subsectionService.GetSubsectionByProjectIdAsync(projectId);
        }

        public async Task<int> GetPtojectIdAsync(string buildingNumber)
        {
            return await _projectService.GetPtojectIdAsync(buildingNumber);
        }

        public async Task<bool> AddSubsectionsAsync(List<SubsectionModel> subsections)
        {
            return await _subsectionService.AddSubsectionsAsync(subsections);
        }

        public async Task<bool> RemoveSubsectionsByProjectIdAsync(int projectId)
        {
           return await _subsectionService.RemoveSubsectionsByProjectIdAsync(projectId);
        }

        public async Task<SubsectionModel> GetSubsectionById(int subsectionId)
        {
            return await _subsectionService.GetSubsectionById(subsectionId);
        }

        public async Task<bool> RemoveSubsectionsByIdAsync(int subsectionId)
        {
            return await _subsectionService.RemoveSubsectionsByIdAsync(subsectionId);
        }

        public async Task<bool> EditSubsectionAsync(SubsectionModel subsection)
        {
            return await _subsectionService.EditSubsectionAsync(subsection);
        }

        public async Task<bool> EditProjectAsync(ProjectModel newProject)
        {
            return await _projectService.EditProjectAsync(newProject);
        }

        public async Task<bool> UpdateCiCVersionAsync(int projectId, string newCiCVersion)
        {
            return await _projectService.UpdateCiCVersionAsync(projectId,newCiCVersion);
        }

        public async Task<bool> UpdateCoefficientsAsync(int projectId, double? newPK1, double? newPK2)
        {
            return await _projectService.UpdateCoefficientsAsync(projectId, newPK1, newPK2);
        }
    }
}
