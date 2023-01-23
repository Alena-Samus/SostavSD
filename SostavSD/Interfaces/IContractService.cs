using SostavSD.Data;
using SostavSD.Models;

namespace SostavSD.Interfaces
{
    public interface IContractService
    {
        //IEnumerable<ContractModel> List{ get; }
        SostavSDContext Context { get; }
    }
}
