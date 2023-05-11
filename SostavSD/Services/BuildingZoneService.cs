using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NLog;
using SostavSD.Data;
using SostavSD.Interfaces;
using SostavSD.Models;

namespace SostavSD.Services
{
    public class BuildingZoneService : IBuildingZoneService
    {
        private readonly SostavSDContext _context;
        private readonly IMapper _mapper;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public BuildingZoneService(SostavSDContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<BuildingZoneModel> GetBuildingZoneByIdAsync(int zoneId)
        {
            var currentZone = _context.buildingZone.FirstOrDefault(x => x.BuildingZoneId == zoneId);

            return  _mapper.Map<BuildingZoneModel>(currentZone);
        }

        public async Task<List<BuildingZoneModel>> GetBuildingZoneModelsAsync()
        {
            try
            {
                var zonesList = _context.buildingZone;

                return _mapper.Map<List<BuildingZoneModel>>(await zonesList.ToListAsync());
            }
            catch (Exception ex)
            {
                _logger.Error(ex.InnerException);
                throw;
            }            
            
        }
    }
}
