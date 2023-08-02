using Microsoft.AspNetCore.Identity;

namespace SostavSD.Entities
{
	public class UserSostav: IdentityUser
	{
		public string Surname { get; set; }
		public string GroupName { get; set; }

        public ICollection<Contract> Contracts { get; set; }
        public ICollection<Estimate> Estimators { get; set; }
    }
}
