using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using Task_ProjectManagementAPI.Data.Models;
using Task_ProjectManagementAPI.Exceptions;
using Task_ProjectManagementAPI.Extensions;
using Task_ProjectManagementAPI.Services.Interfaces;
using Test_Api.Data;
using Test_Api.Data.Models;
using Test_Api.DTOs;

namespace Task_ProjectManagementAPI.Services.Implementations
{
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ProjectService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProjectDto> CreateAsync(CreateProjectDto dto, string userId)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new BadRequestException($"Project name is required");
            var project = _mapper.Map<Project>(dto);
            project.CreatedByUserId = userId;

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProjectDto>(project);
        }

        public async Task<bool> DeleteAsync(int projectId, string userId)
        {
            var project = await _context.Projects
             .SingleOrDefaultAsync(p => p.Id == projectId && p.CreatedByUserId == userId);

            if (project == null) throw new NotFoundException($"Poject with ID {projectId} not found");

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PagedResult<ProjectDto>> GetAllAsync(string userId , ProjectParams parameters)
        {
            var query =_context.Projects.AsNoTracking().Where(p => p.CreatedByUserId == userId).AsQueryable();
            if (!string.IsNullOrEmpty(parameters.Search))
                query = query.Where(p => p.Name.Contains(parameters.Search));
            if (!string.IsNullOrEmpty(parameters.Status) && Enum.TryParse<ProjectStatus>(parameters.Status, out var status))
                query = query.Where(p => p.Status == status);
            var paged = await query.ToPagedResultAsync(parameters.PageNumber, parameters.PageSize);
            return new PagedResult<ProjectDto>
            {
                Data = _mapper.Map<IEnumerable<ProjectDto>>(paged.Data),
                PageNumber = paged.PageNumber,
                PageSize = paged.PageSize,
                TotalCount = paged.TotalCount,
                TotalPages = paged.TotalPages,
            };
        }

        public async Task<ProjectDto?> GetByIdAsync(int projectId, string userId)
        {
            var project = await _context.Projects.AsNoTracking()
             .SingleOrDefaultAsync(p => p.Id == projectId && p.CreatedByUserId == userId);
            if(project == null) throw new NotFoundException($"Project with ID {projectId} not found");
            return _mapper.Map<ProjectDto>(project);
        }

        public async Task<bool> UpdateAsync(UpdateProjectDto dto, string userId)
        {
            var project = await _context.Projects
            .SingleOrDefaultAsync(p => p.Id == dto.Id && p.CreatedByUserId == userId);

            if (project == null) throw new NotFoundException($"Project with ID {dto.Id} not found");

            project.Name = dto.Name;
            project.Description = dto.Description;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
