using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using Task_ProjectManagementAPI.Services.Interfaces;
using Test_Api.Data;
using Test_Api.Data.Models;
using Test_Api.DTOs;

namespace Task_ProjectManagementAPI.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public TaskService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TaskDto> CreateAsync(CreateTaskDto dto, string userId)
        {
            var project = await _context.Projects
            .SingleOrDefaultAsync(p => p.Id == dto.ProjectId && p.CreatedByUserId == userId);

            if (project == null)
                throw new UnauthorizedAccessException();

            var task = _mapper.Map<TaskItem>(dto);
            task.CreatedByUserId = userId;

            await _context.TaskItems.AddAsync(task);
            await _context.SaveChangesAsync();

            return _mapper.Map<TaskDto>(task);
        }

        public async Task<bool> DeleteAsync(int taskId, string userId)
        {
            var task = await _context.TaskItems
             .Include(t => t.Project)
             .SingleOrDefaultAsync(t => t.Id == taskId && t.Project.CreatedByUserId == userId);

            if (task == null) return false;

            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TaskDto?> GetByIdAsync(int taskId, string userId)
        {
            var task = await _context.TaskItems.AsNoTracking()
            .Include(t => t.Project).Include(a => a.CreatedByUser)
            .SingleOrDefaultAsync(t => t.Id == taskId && t.Project.CreatedByUserId == userId);

            return task == null ? null : _mapper.Map<TaskDto>(task);
        }

        public async Task<IEnumerable<TaskDto>> GetByProjectAsync(int projectId, string userId)
        {
            var tasks = await _context.TaskItems
          .Include(t => t.Project)
          .Where(t => t.ProjectId == projectId && t.Project.CreatedByUserId == userId)
          .ToListAsync();

            return _mapper.Map<IEnumerable<TaskDto>>(tasks);
        }

        public async Task<bool> UpdateAsync(UpdateTaskDto dto, string userId)
        {
            var task = await _context.TaskItems
           .Include(t => t.Project)
           .SingleOrDefaultAsync(t => t.Id == dto.Id && t.Project.CreatedByUserId == userId);

            if (task == null) return false;

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.Priority = dto.Priority;
            task.DueDate = dto.DueDate;
            task.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateStatusAsync(UpdateTaskStatusDto dto, string userId)
        {
            var task = await _context.TaskItems
          .Include(t => t.Project)
          .SingleOrDefaultAsync(t => t.Id == dto.Id && t.Project.CreatedByUserId == userId);

            if (task == null) return false;

            task.Status = dto.Status;
            task.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
