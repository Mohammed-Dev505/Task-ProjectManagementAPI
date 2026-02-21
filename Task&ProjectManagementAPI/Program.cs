using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Task_ProjectManagementAPI.Services.Implementations;
using Task_ProjectManagementAPI.Services.Interfaces;
using Test_Api.Data;
using Test_Api.Data.Models;
using Test_Api.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext 
builder.Services.AddDbContext<AppDbContext>(op =>
op.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DbContext")));

// Add Mapping
builder.Services.AddAutoMapper(typeof(UserProfile));

// Add Identity
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

// Add Interface Service
builder.Services.AddTransient<ITaskService, TaskService>();
builder.Services.AddTransient<IProjectService, ProjectService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
