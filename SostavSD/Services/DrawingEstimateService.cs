using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NLog;
using SostavSD.Data;
using SostavSD.Entities;
using SostavSD.Interfaces;
using SostavSD.Models;

namespace SostavSD.Services
{
    public class DrawingEstimateService : IDrawingEstimateService
    {
        private readonly SostavSDContext _context;
        private readonly IMapper _mapper;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public DrawingEstimateService(SostavSDContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<bool> AddDrawingEstimate(DrawingEstimateModel drawingEstimate)
        {
            try
            {
                DrawingEstimate _currentDE = _mapper.Map<DrawingEstimate>(drawingEstimate);
                _context.drawingEstimate.Add(_currentDE);
                await _context.SaveChangesAsync();
                _context.Entry(_currentDE).State = EntityState.Detached;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
