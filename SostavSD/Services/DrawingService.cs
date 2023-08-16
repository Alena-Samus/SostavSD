using AutoMapper;
using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
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

        public DrawingService(SostavSDContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void EditDrawing(DrawingModel currentDrawing)
        {
            Drawing drawingAfterEdit = _mapper.Map<Drawing>(currentDrawing);
            _context.drawing.Entry(drawingAfterEdit).State = EntityState.Modified;
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
    }
}
