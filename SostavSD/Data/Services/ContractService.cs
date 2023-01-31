using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SostavSD.Data.Interfaces;
using SostavSD.Entities;
using SostavSD.Models;


namespace SostavSD.Data.Services
{
    public class ContractService : IContractService
    {
        private readonly SostavSDContext _context;

        private readonly IMapper _mapper;

        public ContractService(SostavSDContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ContractModel>> GetAllContract()
        {
            var contractList = await _context.contract.ToListAsync();
            return _mapper.Map<List<ContractModel>>(contractList);
        }
    }
}
