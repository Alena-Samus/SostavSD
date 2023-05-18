using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SostavSD.Data;
using SostavSD.Interfaces;
using SostavSD.Models;

namespace SostavSD.Services
{
	public class DesignStageService : IDesignStageService
	{
		private readonly SostavSDContext _context;
		private readonly IMapper _mapper;

		public DesignStageService(SostavSDContext context, IMapper mapper)
		{
			_context= context;
			_mapper= mapper;
		}
		public async Task<List<DesignStageModel>> GetAllDesignStageAsync()
		{
			var stages = _context.designStage;
			return _mapper.Map<List<DesignStageModel>>(await stages.ToListAsync());
		}
	}
}
