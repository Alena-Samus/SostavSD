using SostavSD.Models;
using System.ComponentModel.DataAnnotations;

namespace SostavSD.Entities
{
    public class Company
    {
        [Key]
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string CompanyDetails { get; set; }
        public ICollection<Contract> Contracts { get;set; }
    }
}
