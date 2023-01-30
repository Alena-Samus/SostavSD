using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SostavSD.Data.Interfaces;
using SostavSD.Entities;
using SostavSD.Models;


namespace SostavSD.Data.Services
{
    public class ContractService: IContractService
    {
        private readonly SostavSDContext _context;

       private List<Contract> contractList { get; set; } = new List<Contract>();
        public List<ContractModel> contractModelList { get; set; } = new List<ContractModel>();
        private readonly IMapper _mapper;



        public ContractService(SostavSDContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            
        }
        
        public async Task GetAllContract()
        {

            contractList = await _context.contract.ToListAsync();
            contractModelList = _mapper.Map<List<ContractModel>>(contractList);

           //return contractModelList;
        }


    }
}
