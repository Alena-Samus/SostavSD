using SostavSD.Models;

namespace SostavSD.Data.Interfaces
{
    public interface IContractService
    {
        Task<List<ContractModel>> GetAllContract();
    }
}
