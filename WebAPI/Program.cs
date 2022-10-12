using Core.DataAccess;
using Core.DataAccess.V1;
using Core.Interfaces;
using WebAPI.Interfaces;
using WebAPI.Interfaces.V2;
using WebAPI.Repositories;
using WebAPI.Repositories.V2;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register custom service to the IoC container
// Repositories
builder.Services.AddSingleton<IEmployeeRepositoryV2, EmployeeRepositoryV2>();
builder.Services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
// Filters attribute
// builder.Services.AddScoped<Employee_ValidationActionFilter>();

// DAL
builder.Services.AddSingleton<IEmployeeDataAccessV2, EmployeeDataAccessV2>();
builder.Services.AddSingleton<IEmployeeDataAccess, EmployeeDataAccess>();


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
