﻿using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using SostavSD.Classes.Validation;
using SostavSD.Entities;
using SostavSD.Interfaces;
using SostavSD.Models;

namespace SostavSD.Pages.Contracts
{
    public partial class ContractAddNewAndEdit : ComponentBase
    {
        [CascadingParameter] MudDialogInstance AddOrEditContract { get; set; }
        
        [Parameter] public ContractModel Contract { get; set; }

        [Inject] IEntityManagementService EntityManagementService { get; set; }
        [Inject] ISnackbar Snackbar { get; set; }
		[Inject] IStringLocalizer<ContractAddNewAndEdit> localizer { get; set; }


		private ICompanyService _companyService;
        private IAuthorizedUserService _authorizedUserService;
        

        private List<CompanyModel> _companies = new List<CompanyModel>();
        private List<SourceOfFinacingModel> _sources = new List<SourceOfFinacingModel>();
        private List<BuildingZoneModel> _buildingZones = new List<BuildingZoneModel>();

        private List<UsersForListModel> _usersCalculator = new List<UsersForListModel>();
        private List<UsersForListModel> _usersCPE = new List<UsersForListModel>();

        private ContractModelValidation _contractModelValidation = new ContractModelValidation();

        public ContractAddNewAndEdit(ICompanyService companyService,IAuthorizedUserService authorizedUserService)
        {
            _companyService = companyService;
            _authorizedUserService = authorizedUserService;
        }
        protected override async Task OnInitializedAsync()
        {                     
            _companies = await _companyService.GetAllCompany();
            _buildingZones = await EntityManagementService.GetBuildingZoneModelsAsync();
            _sources = await EntityManagementService.GetSourcesOfFinancingModelAsync();
            _usersCPE = _authorizedUserService.GetListUserSostavModelByGroup("8");
            _usersCalculator = _authorizedUserService.GetListUserSostavModelByGroup("6");
        }
        private void Cancel()
        {
            AddOrEditContract.Cancel();
           
        }

        private void Submit()
        {
            var validationResult = _contractModelValidation.Validate(Contract);
            string errors = string.Empty;
            if (validationResult.IsValid) 
            {
				AddOrEditContract.Close(DialogResult.Ok(Contract));
                Snackbar.Add("Done", Severity.Success);
			}
			else
			{
				foreach (var item in validationResult.Errors)
				{
					errors += $"{item} ";
				}
				Snackbar.Add($"{errors}", Severity.Error);
			}

		}


    }
}
