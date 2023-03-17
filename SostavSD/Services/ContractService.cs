using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Utilities;
using SostavSD.Data;
using SostavSD.Entities;
using SostavSD.Interfaces;
using SostavSD.Models;
using System.Runtime.CompilerServices;
using TanvirArjel.Blazor.Extensions;

namespace SostavSD.Services
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
            var contractList = _context.contract
                .Include(c => c.Company)
                .Include(c => c.UserName)
                .AsNoTracking();

            return _mapper.Map<List<ContractModel>>(await contractList.ToListAsync());
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
            var singleContract = await _context.contract.FirstOrDefaultAsync(e => e.ContractID == contractId);

            if (singleContract != null)
            {
                _context.contract.Entry(singleContract).State = EntityState.Detached;
            }
            return _mapper.Map<ContractModel>(singleContract);
        }

        public async Task EditContract(ContractModel currentContract)
        {
            Contract contractAfterEdit = _mapper.Map<Contract>(currentContract);
            _context.contract.Update(contractAfterEdit);
            await _context.SaveChangesAsync();

        }
    }
}
