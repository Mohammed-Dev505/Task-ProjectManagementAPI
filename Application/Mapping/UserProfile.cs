

using AutoMapper;
using Task_ProjectManagementAPI.Application.DTOs;
using Task_ProjectManagementAPI.Domain.Entities;

namespace Task_ProjectManagementAPI.Application.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, RegisterUserDto>().ReverseMap();
            CreateMap<User, LoginDto>().ReverseMap();
            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<Project, CreateProjectDto>().ReverseMap();
            CreateMap<Project, UpdateProjectDto>().ReverseMap();
            CreateMap<ProjectDto, CreateProjectDto>().ReverseMap();
            CreateMap<ProjectDto, UpdateProjectDto>().ReverseMap();
            CreateMap<CreateProjectDto, UpdateProjectDto>().ReverseMap();
            CreateMap<TaskItem, TaskDto>().ForMember(dest => dest.CreatedByUserName , op => op.MapFrom(op => op.CreatedByUser.UserName)).ReverseMap();
            CreateMap<TaskItem, CreateTaskDto>().ReverseMap();
            CreateMap<TaskItem, UpdateTaskDto>().ReverseMap();
            CreateMap<TaskDto, CreateTaskDto>().ReverseMap();
            CreateMap<TaskDto, UpdateTaskStatusDto>().ReverseMap();
            CreateMap<CreateTaskDto, UpdateTaskStatusDto>().ReverseMap();
        }
    }
}
