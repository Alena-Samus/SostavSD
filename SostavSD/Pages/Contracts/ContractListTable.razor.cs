using Microsoft.AspNetCore.Components;
using MudBlazor;
using SostavSD.Data.Interfaces;
using SostavSD.Data.Services;
using SostavSD.Models;




namespace SostavSD.Pages.Contracts;

public partial class ContractListTable : ComponentBase
{
    private List<ContractModel> _contracts = new List<ContractModel>();

    private IContractService _contractService;

    private string searchString = "";

    private ContractModel selectedItem = null;

    private List<ContractModel> selectedItems = new List<ContractModel>();

    private IEnumerable<ContractModel> contracts = new List<ContractModel>();

    public ContractListTable(IContractService contractService)
    {
        _contractService = contractService;
    }

    protected override async Task OnInitializedAsync()
    {
        //_contracts = await _contractService.GetAllContract();
       await GetContrscts();
    }

    private async Task<List<Models.ContractModel>> GetContrscts()
    {
        _contracts =  await _contractService.GetAllContract();
        return _contracts;
    }
    private bool FilterFunc1(ContractModel contract) => FilterFunc(contract, searchString);

    private bool FilterFunc(ContractModel contract, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return true;
        if (contract.ProjectName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (contract.Index.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (contract.Order.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (contract.ContractNumber.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (contract.ContractDate.ToShortDateString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (contract.ContractDateEndOfWork.ToShortDateString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if (contract.City.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        if ($"{contract.ProjectName} {contract.Index} {contract.Order} {contract.ContractNumber} {contract.ContractDate} {contract.ContractDateEndOfWork} {contract.City}".Contains(searchString))
            return true;
        return false;
    }

    private async Task Delete(int contractId)
    {
        bool? result = await _dialogService.ShowMessageBox(
            "Delete",
           "Delete Contract?",
            yesText: "Delete!", cancelText: "Cancel");



        if (result ?? false)
        {
            await _contractService.DeleteContract(contractId);
            await GetContrscts();
        }

    }

    private async Task CreateNewContractAsync()
    {

        var parameters = new DialogParameters();
        parameters.Add("Contract", new ContractModel());


        var dialog = await _dialogService.Show<ContractAddNewAndEdit>("Create a new contract", parameters).Result;

        //if(dialog.Data != null)
        //{
        //    ContractModel newContract = dialog.Data as ContractModel;
        //    _contractService.AddContract(newContract);
        //}
    }


}
