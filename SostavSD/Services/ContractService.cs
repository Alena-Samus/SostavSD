using AutoMapper;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using NLog;
using SostavSD.Data;
using SostavSD.Entities;
using SostavSD.Interfaces;
using SostavSD.Models;


namespace SostavSD.Services
{
    public class ContractService : IContractService
    {
        private readonly SostavSDContext _context;

        private readonly IMapper _mapper;

        private Logger _logger = LogManager.GetCurrentClassLogger();

        public ContractService(SostavSDContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<List<ContractModel>> GetAllContract()
        {
           
            try
            {
                var contractList = _context.contract
                .Include(c => c.Company)
                .Include(c => c.Executor)
                .AsNoTracking();

                return _mapper.Map<List<ContractModel>>(await contractList.ToListAsync());
            }
            catch (Exception ex)
            {
                _logger.Error(ex.InnerException);
                return new List<ContractModel>();
            }
                        
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
            try
            {
                Contract _newContract = _mapper.Map<Contract>(newContract);
                _context.contract.Add(_newContract);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.InnerException);
                
            }
            

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
            try
            {
                Contract contractAfterEdit = _mapper.Map<Contract>(currentContract);
                _context.contract.Update(contractAfterEdit);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                _logger.Error(ex.InnerException);
            }
            

        }

        public async Task<List<ContractModel>> GetCurrentUserContracts(string userId)
        {
            var contractList = _context.contract
                .Where(c => c.UserID == userId)
                .Include(c => c.Company)
                .Include(c => c.Executor)
                .AsNoTracking();

            return _mapper.Map<List<ContractModel>>(await contractList.ToListAsync());
        }
    }
}
