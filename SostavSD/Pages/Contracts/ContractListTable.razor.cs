using Microsoft.AspNetCore.Components;
using SostavSD.Data.Interfaces;
using SostavSD.Models;

namespace SostavSD.Pages.Contracts;

public partial class ContractListTable : ComponentBase
{
    private List<ContractModel> _contracts;

    private IContractService _contractService;

    public ContractListTable(IContractService contractService)
    {
        _contractService = contractService;
    }

    protected override async Task OnInitializedAsync()
    {
        _contracts =  await _contractService.GetAllContract();
    }
}
