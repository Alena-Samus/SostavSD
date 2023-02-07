using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SostavSD.Data.Interfaces;
using SostavSD.Entities;
using SostavSD.Models;
using System.Runtime.CompilerServices;
using TanvirArjel.Blazor.Extensions;

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


        public async Task DeleteContract(int contractId)
        {
            var contractToRemove = await _context.contract.FindAsync(contractId);

            if (contractToRemove != null)
            {
                _context.contract.Remove(contractToRemove);
                _context.SaveChanges();
               
            }
        }

        public void AddContract(ContractModel newContract)
        {
            Contract _newContract = _mapper.Map<Contract>(newContract);
            _context.contract.Add(_newContract);
            _context.SaveChanges();
        }
    }
}
