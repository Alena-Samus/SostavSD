using SostavSD.Entities;
using SostavSD.Models;

namespace SostavSD.Interfaces
{
    public interface IDeppartService
    {
        Task <List<DeppartModel>> GetDeppartsAsync ();
        Task<List<DeppartModel>> GetDeppartsByGroupsAsync(List<string> groups);
    }
}
