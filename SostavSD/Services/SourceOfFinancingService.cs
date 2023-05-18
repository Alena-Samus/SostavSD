using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NLog;
using SostavSD.Data;
using SostavSD.Interfaces;
using SostavSD.Models;

namespace SostavSD.Services
{
    public class SourceOfFinancingService : ISourceOfFinancingService
    {
        private readonly SostavSDContext _context;
        private readonly IMapper _mapper;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public SourceOfFinancingService (SostavSDContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<SourceOfFinacingModel>> GetSourcesOfFinancingModelAsync()
        {
            try
            {
                var sourcesList = _context.sourceOfFinacing
                    .AsNoTracking();

                return _mapper.Map<List<SourceOfFinacingModel>>(await sourcesList.ToListAsync());
            }
            catch (Exception ex)
            {
                _logger.Error(ex.InnerException);
                throw;
            }
        }
    }
}
