using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using SostavSD.Interfaces;
using SostavSD.Models;




namespace SostavSD.Pages.Contracts;

public partial class ContractListTable : ComponentBase
{
    private List<ContractModel> _contracts = new List<ContractModel>();

    private IContractService _contractService;
    private IDialogService _dialogService;
    private IStringLocalizer<ContractListTable> _localizer;



    private string searchString = "";



    public ContractListTable(IContractService contractService, IDialogService dialogService, IStringLocalizer<ContractListTable> localizer)
    {
        _contractService = contractService;
        _dialogService = dialogService;
        _localizer = localizer;
    }


    protected override async Task OnInitializedAsync()
    {
        await GetContracts();        
    }

    private async Task<List<ContractModel>> GetContracts()
    {
        _contracts =  await _contractService.GetAllContract();
        return _contracts;
    }
    private bool FilterFuncCurrent(ContractModel contract) => FilterFunc(contract, searchString);

    private bool FilterFunc(ContractModel contract, string searchString)
    {
        bool result = string.IsNullOrWhiteSpace(searchString)
             || (!string.IsNullOrWhiteSpace(contract.ProjectName) && contract.ProjectName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
             || (!string.IsNullOrWhiteSpace(contract.Index) && contract.Index.Contains(searchString, StringComparison.OrdinalIgnoreCase))
             || (!string.IsNullOrWhiteSpace(contract.Order) && contract.Order.Contains(searchString, StringComparison.OrdinalIgnoreCase))
             || (!string.IsNullOrWhiteSpace(contract.ContractNumber) && contract.ContractNumber.Contains(searchString, StringComparison.OrdinalIgnoreCase))
             || (!string.IsNullOrWhiteSpace(contract.ContractDate.ToString()) && contract.ContractDate.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
             || (!string.IsNullOrWhiteSpace(contract.City) && contract.City.Contains(searchString, StringComparison.OrdinalIgnoreCase))
             || (!string.IsNullOrWhiteSpace(contract.Company.CompanyName) && contract.Company.CompanyName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
             //|| (!string.IsNullOrWhiteSpace(contract.Executor.UserName) && contract.Executor.Surname.Contains(searchString, StringComparison.OrdinalIgnoreCase))
             || ((contract.BuildingZoneId > 1) && contract.BuildingZone.BuildingZoneName.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            //|| (!string.IsNullOrWhiteSpace(contract.Executor.UserName) && contract.Executor.UserName.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            
     
        return result;
    }

    private async Task Delete(int contractId)
    {
        ContractModel currentContractName = await _contractService.GetSingleContract(contractId);
        bool? result = await _dialogService.ShowMessageBox(
            "Удаление договора",
           $"Удалить договор \"{currentContractName.ProjectName}\"?",
            yesText: "Удалить", cancelText: "Отмена");


        if (result ?? false)
        {
            await _contractService.DeleteContract(contractId);
            await GetContracts();
        }

    }

    private async Task Edit(int contractId)
    {
        var parameters = new DialogParameters();
        var contractToEdit = await _contractService.GetSingleContract(contractId);
        parameters.Add("Contract", contractToEdit);
        var dialog = await _dialogService.Show<ContractAddNewAndEdit>("update", parameters).Result;
        if (dialog != null)
        {
            await _contractService.EditContract(contractToEdit);
            await GetContracts();
        }
    }


    private async Task CreateNewContract()
    {

        var parameters = new DialogParameters();
        parameters.Add("Contract", new ContractModel());

        var dialog = await _dialogService.Show<ContractAddNewAndEdit>("add", parameters).Result;

        if (dialog.Data != null)
        {
            ContractModel newContract = (ContractModel)dialog.Data;
            await _contractService.AddContract(newContract);
            await GetContracts();
        }
    }


}
