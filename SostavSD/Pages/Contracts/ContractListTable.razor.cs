﻿using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using SostavSD.Interfaces;
using SostavSD.Models;




namespace SostavSD.Pages.Contracts;

public partial class ContractListTable : ComponentBase
{
    private List<ContractForTableModel> _contractForTableModel = new List<ContractForTableModel>();

    private IContractService _contractService;
    [Inject] IEntityManagementService EntityManagementService { get; set; }
    private IDialogService _dialogService;
    private IStringLocalizer<ContractListTable> _localizer;


    [Inject] IContractForTableService _contractForTableService { get; set; }

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

    private async Task<List<ContractForTableModel>> GetContracts()
    {
        _contractForTableModel.Clear();
        _contractForTableModel = await _contractForTableService.GetContractsAsync();
        return _contractForTableModel;
    }
    private bool FilterFuncCurrent(ContractForTableModel contract) => FilterFunc(contract, searchString);

    private bool FilterFunc(ContractForTableModel contract, string searchString)
    {
        bool result = string.IsNullOrWhiteSpace(searchString)
            || (!string.IsNullOrWhiteSpace(contract.Contract.Index) && contract.Contract.Index.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            || (!string.IsNullOrWhiteSpace(contract.Contract.Order) && contract.Contract.Order.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            || (!string.IsNullOrWhiteSpace(contract.Contract.ContractNumber) && contract.Contract.ContractNumber.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            || (!string.IsNullOrWhiteSpace(contract.Contract.ContractDate.ToString()) && contract.Contract.ContractDate.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
            || (!string.IsNullOrWhiteSpace(contract.Contract.City) && contract.Contract.City.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            || (!string.IsNullOrWhiteSpace(contract.Contract.Company.CompanyName) && contract.Contract.Company.CompanyName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            || (!string.IsNullOrWhiteSpace(contract.Contract.UserID) && contract.Contract.Executor.Surname.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            || ((contract.Contract.BuildingZoneId > 1) && contract.Contract.BuildingZone.BuildingZoneName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            || (!string.IsNullOrWhiteSpace(contract.Calculator.UserSurname) && contract.Calculator.UserSurname.Contains(searchString, StringComparison.OrdinalIgnoreCase))
            ;


        return result;
    }

    private async Task Delete(int contractId)
    {
        ContractModel currentContractName = await _contractService.GetSingleContract(contractId);
        bool? result = await _dialogService.ShowMessageBox(
            "Удаление договора",
           $"Удалить договор \"{currentContractName.ContractNumber}\"?",
            yesText: "Удалить", cancelText: "Отмена");


        if (result ?? false)
        {
            await _contractService.DeleteContract(contractId);
            await GetContracts();
        }

    }

    private async Task Edit(int contractId)
    {
        await EntityManagementService.EditContractDialog(contractId);
        await GetContracts();
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
