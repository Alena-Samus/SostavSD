using SostavSD.Models;

namespace SostavSD.Data.Interfaces
{
    public interface IContractService
    {
        Task<List<ContractModel>> GetAllContract();
        Task DeleteContract(int contractId);
        Task AddContract(ContractModel newContract);

        Task <ContractModel> GetSingleContract(int contractId);

        Task EditContract(ContractModel currentContract);
    }
}
