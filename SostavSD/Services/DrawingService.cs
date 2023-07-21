using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SostavSD.Data;
using SostavSD.Interfaces;
using SostavSD.Models;

namespace SostavSD.Services
{
    public class DrawingService : IDrawingService
    {
        private readonly SostavSDContext _context;
        private readonly IMapper _mapper;

        public DrawingService(SostavSDContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<DrawingModel>> GetDrawingModelsAsync()
        {
            var drawingList = _context.drawing
                .Include(c => c.Project)
                .AsNoTracking();

            return _mapper.Map<List<DrawingModel>>(await drawingList.ToListAsync());
        }
    }
}
