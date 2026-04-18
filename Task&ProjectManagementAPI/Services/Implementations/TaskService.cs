using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Task_ProjectManagementAPI.Data.Models;
using Task_ProjectManagementAPI.Exceptions;
using Task_ProjectManagementAPI.Extensions;
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
                throw new NotFoundException($"Project with ID {dto.ProjectId} not found");

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

            if (task == null) throw new NotFoundException($"Task with ID {taskId} not found");

            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TaskDto?> GetByIdAsync(int taskId, string userId)
        {
            var task = await _context.TaskItems.AsNoTracking()
            .Include(t => t.Project).Include(a => a.CreatedByUser)
            .SingleOrDefaultAsync(t => t.Id == taskId && t.Project.CreatedByUserId == userId);
            if (task == null) throw new NotFoundException($"Task with ID {taskId} not found");
            return _mapper.Map<TaskDto>(task);
        }

        public async Task<PagedResult<TaskDto>> GetByProjectAsync(int projectId, string userId, TaskParams parameters)
        {
            var project = await _context.Projects.AsNoTracking().SingleOrDefaultAsync(p => p.Id == projectId && p.CreatedByUserId == userId);
            if (project == null)
                throw new NotFoundException($"Project with ID {projectId} not found");
            var query = _context.TaskItems.AsNoTracking().Include(t => t.Project).Include(c => c.CreatedByUser).Where(t => t.ProjectId == projectId).AsQueryable();

            if (!string.IsNullOrEmpty(parameters.Search))
                query = query.Where(t => t.Title.Contains(parameters.Search));

            if (!string.IsNullOrEmpty(parameters.Status) && Enum.TryParse<Test_Api.Data.Models.TaskStatus>(parameters.Status, out var status))
                query = query.Where(t => t.Status == status);

            if (!string.IsNullOrEmpty(parameters.Priority) && Enum.TryParse<TaskPriority>(parameters.Priority, out var priority))
                query = query.Where(t => t.Priority == priority);

            var paged = await query.ToPagedResultAsync(parameters.PageNumber, parameters.PageSize);

            return new PagedResult<TaskDto>
            {
                Data = _mapper.Map<IEnumerable<TaskDto>>(paged.Data),
                PageNumber = paged.PageNumber,
                PageSize = paged.PageSize,
                TotalCount = paged.TotalCount,
                TotalPages = paged.TotalPages
            };
        }

        public async Task<bool> UpdateAsync(UpdateTaskDto dto, string userId)
        {
            var task = await _context.TaskItems
           .Include(t => t.Project)
           .SingleOrDefaultAsync(t => t.Id == dto.Id && t.Project.CreatedByUserId == userId);

            if (task == null) throw new NotFoundException($"Task with ID {dto.Id} not found");

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

            if (task == null) throw new NotFoundException($"Task with ID {dto.Id} not found");

            task.Status = dto.Status;
            task.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
