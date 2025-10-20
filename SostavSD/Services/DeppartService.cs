using AutoMapper;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NLog;
using SostavSD.Data;
using SostavSD.Entities;
using SostavSD.Interfaces;
using SostavSD.Models;

namespace SostavSD.Services
{
    public class DeppartService : IDeppartService
    {
        private readonly SostavSDContext _context;
        private readonly IMapper _mapper;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public DeppartService(SostavSDContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<DeppartModel>> GetDeppartsAsync()
        {
            //try
            //{
                var _deppartsList = await _context.deppart
                            .AsNoTracking()                    
                            .ToListAsync();
                
                return _mapper.Map<List<DeppartModel>>(_deppartsList);
            //}
            //catch (Exception ex) 
            //{
            //    _logger.Error(ex);
            //    throw;
            //}
        }

        public async Task<List<DeppartModel>> GetDeppartsByGroupsAsync(List<string> groups)
        {
            //try
            //{
                var drawings = await _context.deppart
                    .Where(d => groups.Contains(d.GroupANU))
                    .AsNoTracking()
                    .ToListAsync();

                return _mapper.Map<List<DeppartModel>>(drawings);
            //}
            //catch (Exception ex)
            //{
            //    _logger.Error(ex);
            //    throw;
            //}
        }
    }
}
