using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NLog;
using SostavSD.Data;
using SostavSD.Interfaces;
using SostavSD.Models;

namespace SostavSD.Services
{
    public class ChapterService : IChapterService
    {
        private readonly SostavSDContext _context;
        private readonly IMapper _mapper;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public ChapterService(SostavSDContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ChapterModel>> GetChaptersByCountryAsync(string country)
        {
            try
            {
                var _chaptersList = _context.chapter
                    .Where(u => u.Country == country)
                    .AsNoTracking();

                return _mapper.Map<List<ChapterModel>>(await _chaptersList.ToListAsync());
            }
            catch (Exception ex) 
            {
                _logger.Error(ex.InnerException);
                throw;
            }
        }
    }
}
