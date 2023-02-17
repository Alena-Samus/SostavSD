using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SostavSD.Data.Interfaces;
using SostavSD.Entities;
using SostavSD.Models;
using System.Diagnostics.Contracts;

namespace SostavSD.Data.Services
{
    public class CompanyService : ICompanyService

    {
        private readonly SostavSDContext _context;

        private readonly IMapper _mapper;


        public CompanyService(SostavSDContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task AddCompany(CompanyModel newCompany)
        {
            Company _newCompany = _mapper.Map<Company>(newCompany);
            _context.company.Add(_newCompany);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCompany(int companyId)
        {
            var companyToRemove = await _context.company.FindAsync(companyId);

            if (companyToRemove != null)
            {
                _context.company.Remove(companyToRemove);
                _context.SaveChanges();

            }
        }

        public async Task EditCompany(CompanyModel currentCompany)
        {
            Company companytAfterEdit = _mapper.Map<Company>(currentCompany);
            _context.company.Update(companytAfterEdit);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CompanyModel>> GetAllCompany()
        {
            var companyList = _context.company
                .AsNoTracking();
               
            return _mapper.Map<List<CompanyModel>>(await companyList.ToListAsync());
        }

        public async Task<CompanyModel> GetSingleCompany(int companytId)
        {
            var singleCompany = await _context.company.FirstOrDefaultAsync(e => e.CompanyID == companytId);

            if (singleCompany != null)
            {
                _context.company.Entry(singleCompany).State = EntityState.Detached;
            }
            return _mapper.Map<CompanyModel>(singleCompany);
        }
    }
}
