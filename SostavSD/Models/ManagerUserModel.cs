namespace SostavSD.Models
{
    public class ManagerUserModel 
    {
        public string UserId { get; set; }
        public string UserSurname { get; set; }
        public string UserEmail { get; set; }
        public List<string> UserRoles { get; set; }

    }
}
