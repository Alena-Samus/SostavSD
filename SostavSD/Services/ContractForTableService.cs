using SostavSD.Interfaces;
using SostavSD.Models;

namespace SostavSD.Services
{
    public class ContractForTableService : IContractForTableService
    {
        private readonly IContractService _contractService;
        private readonly IAuthorizedUserService _authorizedUserService;

        public ContractForTableService(IContractService contractService, IAuthorizedUserService authorizedUserService)
        {
            _contractService = contractService;
            _authorizedUserService = authorizedUserService;
        }

        public async Task<List<ContractForTableModel>> GetContractsAsync()
        {
            List<ContractModel> _contracts = await _contractService.GetAllContract();

            List<ContractForTableModel> _contractForTableModel = new();

            foreach (var contract in _contracts)
            {
                ManagerUserModel currentCalculator = new ManagerUserModel();

                if (contract.CalculatorId is not null)
                {
                    currentCalculator = await _authorizedUserService.GetSingleUser(contract.CalculatorId);
                }

                var current = new ContractForTableModel { Contract = contract, Calculator = currentCalculator };
                _contractForTableModel.Add(current);
            }

            return _contractForTableModel;
        }
    }
}
