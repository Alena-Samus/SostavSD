using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using NLog;
using SostavSD.Data;
using SostavSD.Entities;
using SostavSD.Interfaces;
using SostavSD.Models;
using SostavSD.Pages.Projects;
using System.Diagnostics.Contracts;

namespace SostavSD.Services
{
	public class ProjectService : IProjectService
	{
		private readonly SostavSDContext _context;
		private readonly IMapper _mapper;
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private bool result = true;

        public ProjectService(SostavSDContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<bool> AddProjectAsync(ProjectModel newProject)
		{
            Project _newProject = _mapper.Map<Project>(newProject);
            try
			{
                

                _context.project.Add(_newProject);

                await _context.SaveChangesAsync();
                _context.Entry(_newProject).State = EntityState.Detached;

                return result;
            }
			catch(Exception ex)
			{
                _logger.Error($"{ex.InnerException}, method: AddProjectAsync, _newProject: {_newProject.ProjectName}, message: {ex.Message}"); 

                return false;
            }
			
		}

        public async Task<bool> CheckBuildingNumber(string buildingNumber)
        {
            try
            {
                var _projects = await _context.project.ToListAsync();
                bool result = _projects.Exists(x => x.BuildingNumber.Equals(buildingNumber));
                return result;

            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.InnerException}, method: CheckBuildingNumber, buildingNumber: {buildingNumber}, message: {ex.Message}");

                return false;
            }

        }

        public async Task<bool> DeleteProjectAsync(int id)
		{
            try
            {
                var projectToRemove = await _context.project.FindAsync(id);

                if (projectToRemove != null)
                {
                    _context.project.Remove(projectToRemove);
                    _context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.InnerException}, method: DeleteProjectAsync, buildingId: {id}, message: {ex.Message}");

                return false;

            }
            return result;
		}

		public async Task<bool> EditProjectAsync(ProjectModel newProject)
		{
            try
            {
                var existingProject = await _context.project
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.ProjectId == newProject.ProjectId);
                if (existingProject == null) 
                { 
                    return false;
                }
                _mapper.Map(newProject, existingProject);
                _context.project.Entry(existingProject).State = EntityState.Modified;
                _context.project.Update(existingProject);
                await _context.SaveChangesAsync();
                _context.Entry(existingProject).State = EntityState.Detached;
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.InnerException}, method: EditProjectAsync, ProjectModel: {newProject.ProjectName}, message: {ex.Message}");

                return false;
            }
        }

        public async Task<ProjectModel> GetProjectByIdAsync(int id)
        {
            try
            {
                var project = await _context.project
                .Include(c => c.Contract)
                    .ThenInclude(c => c.Executor)
                .Include(c => c.Contract)
                    .ThenInclude(c => c.SourceOfFinacing)
                .Include(c => c.Contract)
                    .ThenInclude(c => c.BuildingZone)
                .Include(c => c.BuildingView)
                .Include(c => c.Status)
                .Include(c => c.DesignStage)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ProjectId == id);

                _context.project.Entry(project).State = EntityState.Detached;

                if (project == null)
                {
                    return null;                }

                if (project.Contract!= null)
                {
                    _context.contract.Entry(project.Contract).State = EntityState.Detached;
                }

                if (project.BuildingView!= null) 
                {
                    _context.buildingView.Entry(project.BuildingView).State = EntityState.Detached;
                }

                if (project.Status!= null)
                {
                    _context.status.Entry(project.Status).State = EntityState.Detached;
                }
                if (project.DesignStage!= null) 
                {
                    _context.designStage.Entry(project.DesignStage).State = EntityState.Detached;
                }

                return _mapper.Map<ProjectModel>(project);
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.InnerException}, method: GetProjectByIdAsync, ProjectId: {id}, message: {ex.Message}");

                throw;
            }

        }

        public async Task<List<ProjectModel>> GetProjectsAsync()
		{
            try
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
            catch (Exception ex)
            {
                _logger.Error($"{ex.InnerException}, method: GetProjectsAsync, message: {ex.Message}");

                throw;
            }
        }

        public async Task<int> GetPtojectIdAsync(string buildingNumber)
        {
            try
            {
                var _projects = await _context.project
                    .ToListAsync();
                int BuildingId = _projects.FirstOrDefault(x => x.BuildingNumber == buildingNumber).ProjectId;

                return BuildingId;

            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.InnerException}, method: GetPtojectIdAsync, buildingNumber: {buildingNumber}, message: {ex.Message}");

                return 0;
            }

        }

        public async Task<bool> UpdateCiCVersionAsync(int projectId, string newCiCVersion)
        {
            try
            {
                // Вместо создания нового экземпляра, используем Find для получения существующего
                var existingProject = await _context.project.FindAsync(projectId);

                if (existingProject == null)
                {
                    return false; // Обработка ситуации, если проект не найден
                }

                // Обновляем только конкретное поле
                existingProject.CiCVersion = newCiCVersion;

                // Сохраняем изменения в базе данных
                await _context.SaveChangesAsync();

                // Снимаем отслеживание
                _context.Entry(existingProject).State = EntityState.Detached;
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.InnerException}, method: UpdateCiCVersionAsync, projectId: {projectId}, newCiCVersion: {newCiCVersion}, message: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateCoefficientsAsync(int projectId, double? newPK1, double? newPK2)
        {
            try
            {
                var existingProject = await _context.project.FirstOrDefaultAsync(e => e.ProjectId == projectId);

                if (existingProject == null)
                {
                    return false; 
                }

                existingProject.ProjectK1 = newPK1;
                existingProject.ProjectK2 = newPK2;

                await _context.SaveChangesAsync();

                _context.Entry(existingProject).State = EntityState.Detached;
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.InnerException}, method: UpdateCoefficientsAsync, projectId: {projectId}, newCoefficients: {newPK1} and {newPK2} , message: {ex.Message}");
                return false;
            }
        }
    }
}
