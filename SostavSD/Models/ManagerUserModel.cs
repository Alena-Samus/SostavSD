namespace SostavSD.Models
{
    public class ManagerUserModel 
    {
        public string RegistredUserId { get; set; }
        public string RegistredUserSurname { get; set; }
        public string RegistredUserEmail { get; set; }
        public List<string> RegistredUserRoles { get; set; }

    }
}
