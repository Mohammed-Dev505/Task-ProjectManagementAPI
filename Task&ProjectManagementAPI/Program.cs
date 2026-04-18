using Microsoft.AspNetCore.Identity;
using Task_ProjectManagementAPI.Data.Models;
using Test_Api.Mapping;
using Task_ProjectManagementAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add FluentValidation
builder.Services.AddValidationServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerService();




// Add DbContext 
builder.Services.AddDatabase(builder.Configuration);

// Add Mapping
builder.Services.AddAutoMapper(typeof(UserProfile));

// Add Identity
builder.Services.AddIdentityServices();

// Add Authentication
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
builder.Services.AddJwtAuthentication(builder.Configuration);

// Add Interface Service
builder.Services.AddApplicationServices();




var app = builder.Build();



// Add Role By Default

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = { "Admin", "User" };
    foreach(var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGlobalException();

app.UseHttpsRedirection();

app.UseSecutiry();

app.MapControllers();

app.Run();