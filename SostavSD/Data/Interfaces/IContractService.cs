using SostavSD.Models;


namespace SostavSD.Data.Interfaces
{
    public interface IContractService
    {
        List<ContractModel> contractModelList { get; set; }
        Task GetAllContract();

    }
}
