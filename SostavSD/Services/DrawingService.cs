using AutoMapper;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
using NLog;
using SostavSD.Data;
using SostavSD.Entities;
using SostavSD.Interfaces;
using SostavSD.Models;
using SostavSD.Pages.Projects;
using System.Diagnostics.Contracts;

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

        public async Task<bool> AddDrawingsAsync(List<DrawingModel> drawingsList)
        {
            try
            {
                foreach (DrawingModel drawing in drawingsList)
                {
                    Drawing _currentDrawing = _mapper.Map<Drawing>(drawing);
                    _context.drawing.Add(_currentDrawing);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.InnerException);
                throw;
            }

        }

        public async Task EditDrawing(DrawingModel currentDrawing)
        {
            //try
            //{
            //    Drawing drawingAfterEdit = _mapper.Map<Drawing>(currentDrawing);
            //    _context.drawing.Entry(drawingAfterEdit).State = EntityState.Detached; //снимать отслеживание в момент получения 
            //    _context.drawing.Update(drawingAfterEdit);
            //    _context.SaveChanges();
            //}
            //catch (Exception ex) 
            //{
            //    _logger.Error(ex.InnerException);
            //    throw;
            //}

        }

        public async Task<List<DrawingModel>> GetDrawingModelsAsync()
        {
            try
            {
                var drawingList = _context.drawing
                   .Include(c => c.Project)
                   .Include(c => c.Group)
                   .AsNoTracking();

                return _mapper.Map<List<DrawingModel>>(await drawingList.ToListAsync());

            }
            catch (Exception ex)
            {
                _logger.Error(ex.InnerException);
                throw;
            }
        }

        public async Task<List<DrawingModel>> GetDrawingModelByProjectIdAsync(int i)
        {
            try
            {
                var _drawingsById = _context.drawing
                    .Include(c => c.Group)
                    .Include(c => c.Project)
                    .Where(u => u.ProjectId == i);

                return _mapper.Map<List<DrawingModel>>(await _drawingsById.ToListAsync());
            }
            catch (Exception ex)
            {
                _logger.Error(ex.InnerException);

                throw;
            }
        }

        public async Task<bool> RemoveDrawingsAsync(int drawingId)
        {
            try
            {
                var drawingToRemove = await _context.drawing.FindAsync(drawingId);

                if (drawingToRemove != null)
                {
                    _context.drawing.Remove(drawingToRemove);
                    _context.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.InnerException);

                throw;
            }
        }

        public async Task<bool> EditDrawingAsync(DrawingModel currentDrawing)
        {
            try
            {
                Drawing drawingAfterEdit = _mapper.Map<Drawing>(currentDrawing);
                _context.drawing.Entry(drawingAfterEdit).State = EntityState.Modified;
                _context.drawing.Update(drawingAfterEdit);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.InnerException);

                throw;
            }
        }

        public async Task<DrawingModel> GetSingleDrawingById(int drawingId)
        {
            try
            {
                var singleDrawing = await _context.drawing
                .FirstOrDefaultAsync(e => e.DrawingId == drawingId);

                if (singleDrawing != null)
                {
                    _context.drawing.Entry(singleDrawing).State = EntityState.Detached;
                }

                return _mapper.Map<DrawingModel>(singleDrawing);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.InnerException);
                throw;
            }

        }
    }
}
