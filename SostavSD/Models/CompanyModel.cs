namespace SostavSD.Models
{
    public class CompanyModel
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyDetails { get; set; }

       public ICollection<ContractModel> Contracts { get; set; }
    }
}
