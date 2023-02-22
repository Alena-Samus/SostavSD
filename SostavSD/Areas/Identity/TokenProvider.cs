namespace SostavSD.Areas.Identity
{
	public class TokenProvider
	{
		public string XsrfToken { get; set; }
		public string RefreshSecret { get;set; }
	}
}
