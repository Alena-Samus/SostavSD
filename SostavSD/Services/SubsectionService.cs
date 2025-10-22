using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
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

        public async Task<List<SubsectionModel>> GetSubsectionByProjectIdAsync(int projectId)
        {
            try
            {
                var _subsectionsById = _context.section
                    .Include(c => c.Chapter)
                    .Include(c => c.Project)
                    .Where(u => u.ProjectId == projectId);

                return _mapper.Map<List<SubsectionModel>>(await _subsectionsById.ToListAsync());
            }
            catch (Exception ex)
            {
                _logger.Error(ex.InnerException);

                throw;
            }
        }

        public async Task<bool> RemoveSubsectionsAsync(int projectId)
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
