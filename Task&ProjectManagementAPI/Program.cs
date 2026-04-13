using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Task_ProjectManagementAPI.Data.Models;
using Task_ProjectManagementAPI.Exceptions;
using Task_ProjectManagementAPI.Middleware;
using Task_ProjectManagementAPI.Services.Implementations;
using Task_ProjectManagementAPI.Services.Interfaces;
using Test_Api.Data;
using Test_Api.Data.Models;
using Test_Api.Mapping;
using FluentValidation;
using FluentValidation.AspNetCore;
using Task_ProjectManagementAPI.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateProjectValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter:Bearer {token}"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));


// Add DbContext 
builder.Services.AddDbContext<AppDbContext>(op =>
op.UseSqlServer(builder.Configuration.GetConnectionString("DbContext")));

// Add Mapping
builder.Services.AddAutoMapper(typeof(UserProfile));

// Add Identity
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

// Add Authentication

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var jwtsettings = builder.Configuration.GetSection("JWT");
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtsettings["Issuer"],
        ValidAudience = jwtsettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtsettings["Key"]))
    };
});


// Add Interface Service
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddTransient<ExceptionMiddleware>();




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

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
