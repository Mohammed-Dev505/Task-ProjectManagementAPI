using Application.Services.Interfaces;
using AutoMapper;
using System.Linq.Expressions;
using Task_ProjectManagementAPI.Application.DTOs;
using Task_ProjectManagementAPI.Application.Exceptions;
using Task_ProjectManagementAPI.Application.Models;
using Task_ProjectManagementAPI.Application.Services.Interfaces;
using Task_ProjectManagementAPI.Domain.Entities;

namespace Task_ProjectManagementAPI.Application.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProjectService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProjectDto> CreateAsync(CreateProjectDto dto, string userId)
        {
            var project = _mapper.Map<Project>(dto);
            project.CreatedByUserId = userId;

           await _unitOfWork.Repository<Project>().AddAsync(project);

            var saveResult = await _unitOfWork.CompleteAsync();
            if (saveResult < 1)
                throw new BadRequestException("Failed to create the project record.");

            return _mapper.Map<ProjectDto>(project);
        }

        public async Task<bool> DeleteAsync(Guid projectId, string userId)
        {
            var projectRepo = _unitOfWork.Repository<Project>();
            var project = await projectRepo.FindAsync(p => p.Id == projectId && p.CreatedByUserId == userId);

            if (project is null)
                throw new NotFoundException("Project not found or you do not have permission to delete it.");

            projectRepo.Delete(project);

            var saveResult = await _unitOfWork.CompleteAsync();
            if (saveResult < 1)
                throw new BadRequestException("Failed to delete the project.");

            return true;
        }

        public async Task<PagedResult<ProjectDto>> GetAllAsync(string userId , ProjectParams parameters)
        {
            int skip = (parameters.PageNumber - 1) * parameters.PageSize;
            int take = parameters.PageSize;

            Expression<Func<Project, bool>> filter = p => (string.IsNullOrEmpty(parameters.projectName) || p.Name.Contains(parameters.projectName)) ||
                                                          (string.IsNullOrEmpty(parameters.Status) || p.Status.ToString() == parameters.Status);

            var projectRepo = _unitOfWork.Repository<Project>();

            var projects = await projectRepo.GetPagedResultAsync(filter, skip, take);

            int totalCount = await projectRepo.CountAsync(filter);

            var projectDto = _mapper.Map<IEnumerable<ProjectDto>>(projects);

            return new PagedResult<ProjectDto>
            {
                Data = projectDto.ToList(),
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize,
                TotalCount = totalCount
            };
        }

        public async Task<ProjectDto?> GetByIdAsync(Guid projectId, string userId)
        {
            var project = await _unitOfWork.Repository<Project>().FindAsync(p => p.Id == projectId && p.CreatedByUserId == userId);

            if (project is null)
                throw new NotFoundException("Project not found or you not have permission to access it.");

            return _mapper.Map<ProjectDto>(project);
        }

        public async Task<bool> UpdateAsync(Guid id , UpdateProjectDto dto, string userId)
        {
            var projectRepo = _unitOfWork.Repository<Project>();
            var project = await projectRepo.FindAsync(p => p.Id == id && p.CreatedByUserId == userId);

            if (project is null)
                throw new NotFoundException("Project not found or you not have permission to update it.");

            project.Name = dto.Name;
            project.Description = dto.Description;

            projectRepo.Update(project);

            var saveResult = await _unitOfWork.CompleteAsync();
            if (saveResult < 1)
                throw new BadRequestException("Failed to update the project details.");

            return true;
            
        }
    }
}
