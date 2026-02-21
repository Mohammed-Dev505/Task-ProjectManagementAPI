using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
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

            if (project == null) return false;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllAsync(string userId)
        {
            var projects = await _context.Projects.AsNoTracking()
             .Where(p => p.CreatedByUserId == userId)
             .ToListAsync();

            return _mapper.Map<IEnumerable<ProjectDto>>(projects);
        }

        public async Task<ProjectDto?> GetByIdAsync(int projectId, string userId)
        {
            var project = await _context.Projects.AsNoTracking()
             .SingleOrDefaultAsync(p => p.Id == projectId && p.CreatedByUserId == userId);

            return project == null ? null : _mapper.Map<ProjectDto>(project);
        }

        public async Task<bool> UpdateAsync(UpdateProjectDto dto, string userId)
        {
            var project = await _context.Projects
            .SingleOrDefaultAsync(p => p.Id == dto.Id && p.CreatedByUserId == userId);

            if (project == null) return false;

            project.Name = dto.Name;
            project.Description = dto.Description;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
