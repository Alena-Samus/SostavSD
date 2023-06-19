using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SostavSD.Data;
using SostavSD.Interfaces;
using SostavSD.Models;

namespace SostavSD.Services
{
	public class StatusService : IStatusService
	{
		private readonly SostavSDContext _context;
		private readonly IMapper _mapper;

		public StatusService(SostavSDContext context, IMapper mapper)
		{
			_context= context;
			_mapper= mapper;
		}
		public async Task<List<StatusModel>> GetAllStatusAsync()
		{
			var statuses =  _context.status.AsNoTracking();

			return _mapper.Map<List<StatusModel>>(await statuses.ToListAsync());
		}
	}
}
