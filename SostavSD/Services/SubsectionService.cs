using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.Build.Evaluation;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NLog;
using SostavSD.Data;
using SostavSD.Entities;
using SostavSD.Interfaces;
using SostavSD.Models;

namespace SostavSD.Services
{
    public class SubsectionService : ISubsectionService
    {
        private readonly SostavSDContext _context;

        private readonly IMapper _mapper;

        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        private bool result = true;

        public SubsectionService(SostavSDContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<bool> AddSubsectionsAsync(List<SubsectionModel> subsections)
        {
            try
            {
                foreach(SubsectionModel item in subsections)
                {
                    Subsection _newSubsection = _mapper.Map<Subsection>(item);
                    _context.section.Add(_newSubsection);
                    await _context.SaveChangesAsync();
                    _context.section.Entry(_newSubsection).State = EntityState.Detached;

                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.InnerException);
                return false;
                throw;

            }
            return result;
        }

        public async Task<bool> EditSubsectionAsync(SubsectionModel subsection)
        {
            try
            {
                Subsection subsectionAfterEdit = _mapper.Map<Subsection>(subsection);
                _context.section.Entry(subsectionAfterEdit).State = EntityState.Modified;
                _context.section.Update(subsectionAfterEdit);
                await _context.SaveChangesAsync();

                _context.section.Entry(subsectionAfterEdit).State = EntityState.Detached;
                return true;

            }
            catch (Exception ex)
            {
                _logger.Error(ex.InnerException);

                throw;
            }
        }

        public async Task<SubsectionModel> GetSubsectionById(int subsectionId)
        {
            
            try
            {
                var _subsectionsById = await _context.section
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(u => u.SubsectionId == subsectionId);
                if (_subsectionsById != null)
                {
                    _context.section.Entry(_subsectionsById).State = EntityState.Detached;
                }

                return _mapper.Map<SubsectionModel>(_subsectionsById);

            }
            catch (Exception ex)
            {
                _logger.Error(ex.InnerException); 
                throw;
            }
        }

        public async Task<List<SubsectionModel>> GetSubsectionByProjectIdAsync(int projectId)
        {

            try
            {
                var _subsectionsById = _context.section
                    .Include(c => c.Chapter)
                    .Include(c => c.Project)
                    .AsNoTracking()
                    .Where(u => u.ProjectId == projectId);

                return _mapper.Map<List<SubsectionModel>>(await _subsectionsById.ToListAsync());
            }
            catch (Exception ex)
            {
                _logger.Error(ex.InnerException);

                throw;
            }
        }

        public async Task<bool> RemoveSubsectionsByIdAsync(int subsectionId)
        {
            try
            {
                var _subsectionsToRemove = await _context.section.FirstOrDefaultAsync(u => u.SubsectionId == subsectionId);

                if (_subsectionsToRemove != null)
                {                    
                    _context.section.Remove(_subsectionsToRemove);
                    _context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.InnerException);

                return false;

                throw;
            }
            return result;
        }

        public async Task<bool> RemoveSubsectionsByProjectIdAsync(int projectId)
        {

            try
            {
                var _subsectionsToRemove = await _context.section.Where(u => u.ProjectId == projectId).ToListAsync();

                if (_subsectionsToRemove != null)
                {
                    foreach(var subsection in _subsectionsToRemove)
                    {
                        _context.section.Remove(subsection);
                    }
                    _context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.InnerException);

                return false;

                throw;
            }
            return result;
        }
    }
}
