using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NLog;
using SostavSD.Data;
using SostavSD.Entities;
using SostavSD.Interfaces;
using SostavSD.Models;

namespace SostavSD.Services
{
    public class EstimateService : IEstimateService
    {
        private readonly SostavSDContext _context;
        private readonly IMapper _mapper;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public EstimateService(SostavSDContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        
        public async Task<int> AddEstimateAsync(EstimateModel estimate)
        {
            try
            {
                Estimate _currentEstimate = _mapper.Map<Estimate>(estimate);
                _context.estimate.Add(_currentEstimate);
                await _context.SaveChangesAsync();
                int isertedId = _currentEstimate.EstimateId;
                _context.Entry(_currentEstimate).State = EntityState.Detached;
               
                return isertedId;
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.InnerException}, method: AddEstimateAcync, estimate: {estimate.EstimateName}, message: {ex.Message}");
                return 0;
            }
        }
    }
}
