using Microsoft.AspNetCore.Identity;

namespace SostavSD.Models
{
    public class UserSostavModel : IdentityUser
    {
        public string Surname { get; set; }
        public string GroupName { get; set; }

        public ICollection<ContractModel> Contracts { get; set; }

    }
}
