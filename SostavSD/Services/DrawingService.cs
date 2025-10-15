using AutoMapper;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
using NLog;
using SostavSD.Data;
using SostavSD.Entities;
using SostavSD.Interfaces;
using SostavSD.Models;

namespace SostavSD.Services
{
    public class DrawingService : IDrawingService
    {
        private readonly SostavSDContext _context;
        private readonly IMapper _mapper;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public DrawingService(SostavSDContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddDrawings(List<DrawingModel> drawingsList)
        {
            foreach (DrawingModel drawing in drawingsList)
            {
                Drawing _currentDrawing = _mapper.Map<Drawing>(drawing);
                _context.drawing.Add(_currentDrawing);
                await _context.SaveChangesAsync();
            }
        }

        public async Task  EditDrawing(DrawingModel currentDrawing)
        {
            Drawing drawingAfterEdit = _mapper.Map<Drawing>(currentDrawing);
            _context.drawing.Entry(drawingAfterEdit).State = EntityState.Detached; //снимать отслеживание в момент получения 
            _context.drawing.Update(drawingAfterEdit);           
            _context.SaveChanges();
            
        }

        public async Task<List<DrawingModel>> GetDrawingModelsAsync()
        {
            var drawingList = _context.drawing
                .Include(c => c.Project)
                .AsNoTracking();

            return _mapper.Map<List<DrawingModel>>(await drawingList.ToListAsync());
        }

        public async Task<List<DrawingModel>> GetDrawingModelsByIdAsync(int i)
        {
            try
            {
                var _drawingsById = _context.drawing.Where( u => u.ProjectId == i);

                return _mapper.Map<List<DrawingModel>>(await _drawingsById.ToListAsync());
            }
            catch (Exception ex)
            {
                _logger.Error(ex.InnerException);

                throw;
            }
        }
    }
}
