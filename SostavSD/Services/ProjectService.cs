using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SostavSD.Data;
using SostavSD.Entities;
using SostavSD.Interfaces;
using SostavSD.Models;
using System.Diagnostics.Contracts;

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

		public async Task<bool> AddProjectAsync(ProjectModel newProject)
		{
			Project _newProject = _mapper.Map<Project>(newProject);

			_context.project.Add(_newProject);

			await _context.SaveChangesAsync();

			return true;
		}

		public async Task<bool> DeleteProjectAsync(int id)
		{
			var projectToRemove = await _context.project.FindAsync(id);

			if (projectToRemove != null)
			{
				_context.project.Remove(projectToRemove);
				_context.SaveChanges();
			}

			return true;
		}

		public async Task<bool> EditProjectAsync(ProjectModel newProject)
		{
			Project projectAfterEdit = _mapper.Map<Project>(newProject);
            _context.project.Entry(projectAfterEdit).State = EntityState.Modified;
            _context.project.Update(projectAfterEdit);
            await _context.SaveChangesAsync();

            return true;
		}

        public async Task<ProjectModel> GetProjectByIdAsync(int id)
        {
	
			var project = _context.project
				.Include(c => c.Contract)
					.ThenInclude(c => c.Executor)
				.Include(c => c.BuildingView)
				.Include(c => c.Status)
				.Include(c => c.DesignStage)
				.AsNoTracking()
				.FirstOrDefault(c => c.ProjectId == id);

			if (project != null)
			{
				_context.project.Entry(project).State = EntityState.Modified;
				_context.contract.Entry(project.Contract).State = EntityState.Modified;
				_context.designStage.Entry(project.DesignStage).State = EntityState.Modified;
				_context.status.Entry(project.Status).State = EntityState.Modified;
				_context.buildingView.Entry(project.BuildingView).State = EntityState.Modified;
			}

			return _mapper.Map<ProjectModel>(project);

        }

        public async Task<List<ProjectModel>> GetProjectsAsync()
		{
			var projectList = _context.project
				.Include(c => c.Contract)
					.ThenInclude(c => c.Executor)
				.Include(c => c.BuildingView)
				.Include(c => c.Status)
				.Include(c => c.DesignStage)
				.AsNoTracking();
            
            return _mapper.Map<List<ProjectModel>>(await projectList.ToListAsync());
		}

	}
}
