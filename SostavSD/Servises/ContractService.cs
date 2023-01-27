using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SostavSD.Data;
using SostavSD.Interfaces;
using SostavSD.Models;
using SostavSD.Entity;
using AutoMapper;

namespace SostavSD.Servises
{
    public class ContractService : IContractService
    {
       private SostavSDContext _context;
       public List<Contract> contractList = new List<Contract>();
       public List<ContractModel> contractModelList = new List<ContractModel>();
       public IMapper _mapper;



        public ContractService(SostavSDContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<ContractModel> GetAll()
        {
            
            contractList = _context.contract.ToList();
            contractModelList = _mapper.Map<List<ContractModel>>(contractList);

            return contractModelList;
        }

    }
    
}
