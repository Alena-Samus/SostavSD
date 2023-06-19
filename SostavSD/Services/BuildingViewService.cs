using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SostavSD.Data;
using SostavSD.Interfaces;
using SostavSD.Models;

namespace SostavSD.Services
{
	public class BuildingViewService : IBuildingViewService
	{
		private readonly SostavSDContext _context;
		private readonly IMapper _mapper;

		public BuildingViewService(SostavSDContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public List<BuildingViewModel> GetAllBuildingView()
		{
			var viewes = _context.buildingView
				.AsNoTracking();
			return _mapper.Map<List<BuildingViewModel>>(viewes.ToList());
		}
	}
}
