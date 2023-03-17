namespace SostavSD.Models
{
    public class UserSostavModel
    {
        public string Surname { get; set; }
        public string GroupName { get; set; }

        public ICollection<ContractModel> Contracts { get; set; }

    }
}
