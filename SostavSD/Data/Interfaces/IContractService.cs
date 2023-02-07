using SostavSD.Models;

namespace SostavSD.Data.Interfaces
{
    public interface IContractService
    {
        Task<List<ContractModel>> GetAllContract();
        Task DeleteContract(int contractId);
        void AddContract(ContractModel newContract);
    }
}
