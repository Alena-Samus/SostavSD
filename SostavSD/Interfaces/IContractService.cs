﻿using SostavSD.Models;

namespace SostavSD.Interfaces
{
    public interface IContractService
    {
        Task<List<ContractModel>> GetAllContract();
        Task DeleteContract(int contractId);
        Task AddContract(ContractModel newContract);

        Task<ContractModel> GetSingleContract(int contractId);
        Task<List<ContractModel>> GetCurrentUserContracts(string userId);
        Task EditContract(ContractModel currentContract);
    }
}
