<<<<<<< HEAD
using Core.DataAccess;
using Core.DataAccess.V1;
using Core.Interfaces;
using WebAPI.Interfaces;
using WebAPI.Interfaces.V2;
using WebAPI.Repositories;
using WebAPI.Repositories.V2;
=======
using Microsoft.AspNetCore.Mvc.Versioning;
using WebAPI.Filters;
using WebAPI.Interfaces;
using WebAPI.Repositories;
using WebAPI.Services;
>>>>>>> feature

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

<<<<<<< HEAD
=======
// API versioning
// builder.Services.AddApiVersioning(options =>
// {
//     options.ReportApiVersions = true;
//     options.AssumeDefaultVersionWhenUnspecified = true;
//     options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
//     options.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");
// });

>>>>>>> feature
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

<<<<<<< HEAD
// Register custom service to the IoC container
// Repositories
builder.Services.AddSingleton<IEmployeeRepositoryV2, EmployeeRepositoryV2>();
builder.Services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
// Filters attribute
// builder.Services.AddScoped<Employee_ValidationActionFilter>();

// DAL
builder.Services.AddSingleton<IEmployeeDataAccessV2, EmployeeDataAccessV2>();
builder.Services.AddSingleton<IEmployeeDataAccess, EmployeeDataAccess>();
=======
// Register custom services to the IoC container for Dependency Injection
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<EmployeeDuplicationActionFilter>();
>>>>>>> feature


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
