using AutoMapper;
using Test_Api.Data.Models;
using Test_Api.DTOs;

namespace Test_Api.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, RegisterUserDto>().ReverseMap();
            CreateMap<User, LoginDto>().ReverseMap();
            CreateMap<UserDto, RegisterUserDto>().ReverseMap();
            CreateMap<UserDto, LoginDto>().ReverseMap();
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
