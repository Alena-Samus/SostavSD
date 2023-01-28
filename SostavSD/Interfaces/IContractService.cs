using SostavSD.Data;
using SostavSD.Models;


namespace SostavSD.Interfaces
{
    public interface IContractService
    {
        List<ContractModel> GetAll();
    }
}
