using SostavSD.Models;

namespace SostavSD.Interfaces
{
    public interface IContractForTableService
    {
        Task<List<ContractForTableModel>> GetContractsAsync();
		Task<ContractForTableModel> GetContractByIdAsync(int contractId);
	}
}
