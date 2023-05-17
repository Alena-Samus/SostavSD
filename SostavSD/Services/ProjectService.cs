using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SostavSD.Data;
using SostavSD.Interfaces;
using SostavSD.Models;

namespace SostavSD.Services
{
	public class ProjectService : IProjectService
	{
		private readonly SostavSDContext _context;
		private readonly IMapper _mapper;

		public ProjectService(SostavSDContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<List<ProjectModel>> GetProjectsAsync()
		{
			var projectList = _context.project
				.Include(c => c.Contract)
					.ThenInclude(c => c.Executor)
				.Include(c => c.BuildingView)
				.Include(c => c.Status)
				.Include(c => c.DesignStage);


			return _mapper.Map<List<ProjectModel>>(await projectList.ToListAsync());
		}
	}
}
