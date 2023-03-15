using Microsoft.AspNetCore.Identity;

namespace SostavSD.Entities
{
	public class UserSostav: IdentityUser
	{
		public string Surname { get; set; }
	}
}
