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

        public async Task AddContract(ContractModel newContract)
        {
            Contract _newContract = _mapper.Map<Contract>(newContract);
             _context.contract.Add(_newContract);
            await _context.SaveChangesAsync();
             
        }

        public async Task<ContractModel> GetSingleContract(int contractId)
        {
            var singleContract = await _context.contract.FindAsync(contractId);
            return _mapper.Map<ContractModel>(singleContract);

        }

        public async Task EditContract(ContractModel currentContract, int contractId)
        {
            Contract oldContract = await _context.contract.FindAsync(contractId);

            oldContract.ProjectName = currentContract.ProjectName;
            oldContract.Index = currentContract.Index;
            oldContract.Order = currentContract.Order;
            oldContract.ContractNumber = currentContract.ContractNumber;
            oldContract.ContractDate = currentContract.ContractDate;
            oldContract.ContractDateEndOfWork = currentContract.ContractDateEndOfWork;
            oldContract.City = currentContract.City;
            await _context.SaveChangesAsync();
        }
    }
}
