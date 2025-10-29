using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NLog;
using SostavSD.Data;
using SostavSD.Interfaces;
using SostavSD.Models;
using SostavSD.Pages.Projects;

namespace SostavSD.Services
{
    public class CoefficientService: ICoefficientService
    {
        private readonly SostavSDContext _context;
        private readonly IMapper _mapper;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public CoefficientService(SostavSDContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CoefficientModel>> GetCoefficiensByBuildingViewIdBuildingZoneId(int buildingViewId, int buildingZoneId)
        {

            var _coefficients = _context.coefficient
                .Where(x => x.BuildingViewId == buildingViewId && x.BuildingZoneId == buildingZoneId);
            try
            {
                return _mapper.Map<List<CoefficientModel>>(await _coefficients.ToListAsync());

            }
            catch (Exception ex) 
            {
                _logger.Error($"{ex.InnerException}, method: GetCoefficiensByBuildingViewIdBuildingZoneId, buildingViewId: {buildingViewId}, " +
                    $"buildingZoneId: {buildingZoneId}, message: {ex.Message}");
                throw;
            }
        }
    }
}
