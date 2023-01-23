using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SostavSD.Data;
using SostavSD.Interfaces;

namespace SostavSD.Servises
{
    public class ContractService : IContractService
    {
        public SostavSDContext _context;

        public ContractService(SostavSDContext context)
        {
            _context = context;
        }

        public SostavSDContext Context
        {
            get => _context;
        }
        //public async Task<IActionResult> Index()
        //{


        //    //return _context.Contract != null ?
        //    //            View(await _context.ContractModel.ToListAsync()) :
        //    //            Problem("Entity set 'SostavSDContext.Contract'  is null.");
        //}
    }
    
}
