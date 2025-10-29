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
        private bool result = true;

        public ChapterService(SostavSDContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<ChapterModel>> GetChaptersByCountryAsync(string country)
        {
            try
            {
                var _chapters = _context.chapter
                    .Where(x => x.Country == country);
                return _mapper.Map<List<ChapterModel>>(await _chapters.ToListAsync());
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }
    }
}
