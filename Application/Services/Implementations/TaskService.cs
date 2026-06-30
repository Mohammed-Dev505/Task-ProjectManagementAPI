using Application.Services.Interfaces;
using AutoMapper;
using Task_ProjectManagementAPI.Application.DTOs;
using System.Linq.Expressions;
using Task_ProjectManagementAPI.Application.Exceptions;
using Task_ProjectManagementAPI.Application.Models;
using Task_ProjectManagementAPI.Application.Services.Interfaces;
using Task_ProjectManagementAPI.Domain.Entities;
using System.Diagnostics.SymbolStore;
namespace Task_ProjectManagementAPI.Application.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TaskService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TaskDto> CreateAsync(Guid projectId, CreateTaskDto dto, string userId)
        {
            var projectExists =await _unitOfWork.Repository<Project>().AnyAsync(p => p.Id == projectId && p.CreatedByUserId == userId);

            if (!projectExists)
                throw new NotFoundException("Project not found or you don't have permission to add tasks to it.");

            var task = _mapper.Map<TaskItem>(dto);
            task.CreatedByUserId = userId;

            await _unitOfWork.Repository<TaskItem>().AddAsync(task);

            var saveResult = await _unitOfWork.CompleteAsync();
            if (saveResult < 1)
                throw new BadRequestException("Failed to create the task record.");

            return _mapper.Map<TaskDto>(task);
        }

        public async Task<bool> DeleteAsync(Guid taskId, string userId)
        {
            var taskRepo = _unitOfWork.Repository<TaskItem>();
            var task = await taskRepo.FindAsync(t => t.Id == taskId && t.Project.CreatedByUserId == userId);

            if (task is null) throw new NotFoundException("Task not found ro you don't have permission to delete it.");

            taskRepo.Delete(task);

            var saveResult = await _unitOfWork.CompleteAsync();
            if (saveResult < 1)
                throw new BadRequestException("Failed to delete the task.");

            return true;
        }

        public async Task<TaskDto?> GetByIdAsync(Guid taskId, string userId)
        {
            var task = await _unitOfWork.Repository<TaskItem>().FindAsync(t => t.Id == taskId && t.CreatedByUserId == userId);

            if (task is null)
                throw new NotFoundException("Task not found or you don't have permission to access it.");

            return _mapper.Map<TaskDto>(task);
        }

        public async Task<PagedResult<TaskDto>> GetByProjectAsync(Guid projectId, string userId, TaskParams parameters)
        {
            int skip = (parameters.PageNumber - 1) * parameters.PageSize;
            int take = parameters.PageSize;

            var projectRepo = _unitOfWork.Repository<Project>();
            var project = await projectRepo.FindAsync(p => p.Id == projectId && p.CreatedByUserId == userId);

            if (project is null)
                throw new NotFoundException("Project not found or you don't have permission to view its tasks.");

            Expression<Func<TaskItem, bool>> filter = t => t.ProjectId == projectId && (string.IsNullOrEmpty(parameters.Title) || t.Title.Contains(parameters.Title)) &&
                                                                                       (string.IsNullOrEmpty(parameters.Status) || t.Status.ToString() == parameters.Status) &&
                                                                                       (string.IsNullOrEmpty(parameters.Priority) || t.Priority.ToString() == parameters.Priority);

            var taskRepo = _unitOfWork.Repository<TaskItem>();

            var tasks = await taskRepo.GetPagedResultAsync(filter, skip, take);

            int totalCount = await taskRepo.CountAsync(filter);

            var taskDto = _mapper.Map<IEnumerable<TaskDto>>(tasks);

            return new PagedResult<TaskDto>
            {
                Data = taskDto.ToList(),
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize,
                TotalCount = totalCount
            };
        }

        public async Task<bool> UpdateAsync(Guid id , UpdateTaskDto dto, string userId)
        {
            var taskRepo = _unitOfWork.Repository<TaskItem>();
            var task = await taskRepo.FindAsync(t => t.Id == id && t.Project.CreatedByUserId == userId);

            if (task is null)
                throw new NotFoundException("Task not found or you don't have permission to update it.");

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.Priority = dto.Priority;
            task.DueDate = dto.DueDate;
            task.UpdatedAt = DateTime.UtcNow;

            taskRepo.Update(task);

            var saveResult = await _unitOfWork.CompleteAsync();
            if (saveResult < 1)
                throw new BadRequestException("Failed to update the task details.");

            return true;
        }

        public async Task<bool> UpdateStatusAsync(Guid id , UpdateTaskStatusDto dto, string userId)
        {
            var taskRepo = _unitOfWork.Repository<TaskItem>();

            var task = await taskRepo.FindAsync(t => t.Id == id && t.Project.CreatedByUserId == userId);

            if (task is null) throw new NotFoundException("Task not found or you don't have permision to modify its status.");

            task.Status = dto.Status;
            task.UpdatedAt = DateTime.UtcNow;

            taskRepo.Update(task);

            var saveResult = await _unitOfWork.CompleteAsync();
            if (saveResult < 1)
                throw new BadRequestException("Failed to update the task status.");

            return true;
        }
    }
}
