using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Task_ProjectManagementAPI.Application.Models;
using Task_ProjectManagementAPI.Domain.Entities;
using Task_ProjectManagementAPI.Extensions;
using Task_ProjectManagementAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerService();

// Add DbContext 
builder.Services.AddDatabase(builder.Configuration);


// Add Authentication
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
builder.Services.AddJwtAuthentication(builder.Configuration);

// Add Application Service
builder.Services.AddApplicationServices();
builder.Services.AddValidationServices();


builder.Services.AddTransient<ExceptionMiddleware>();

var app = builder.Build();



// Add Role By Default

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    await ContextSeed.SeedRolesAsync(roleManager);
    await ContextSeed.CreateAsmin(userManager);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();