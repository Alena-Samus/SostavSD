using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SostavSD.Data;
using SostavSD.Interfaces;
using SostavSD.Models;
using System.Diagnostics.Contracts;

namespace SostavSD.Servises
{
    public class ContractService : IContractService
    {
       private SostavSDContext _context;

        public ContractService(SostavSDContext context)
        {
            _context = context;
        }

        public List<ContractModel> GetAll()
        {
           return _context.Contract.ToList();
        }

    }
    
}
