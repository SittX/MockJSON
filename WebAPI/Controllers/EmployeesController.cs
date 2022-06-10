using Core.Models;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Interfaces;

namespace WebAPI;

[ApiVersion("1.0")]
[ApiController]
[Route("/api/[controller]")]
public class EmployeesController : ControllerBase
{
    private IRepository<Employee> _repo;
    public EmployeesController(IRepository<Employee> repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployeesAsync()
    {
        var employees = await _repo.GetAllAsync();
        if (employees is not null)
        {
            return Ok(employees);
        }
        return BadRequest();
    }

    [HttpGet]
    [Route("findEmployee")]
    public async Task<IActionResult> GetById([FromQuery] string id)
    {
        var employee = await _repo.FindAsync(id);
        if (employee is not null)
        {
            return Ok(employee);
        }
        return BadRequest("User with the given Id cannot be found!");
    }

    [HttpPost]
    [Route("newEmployee")]
    [ServiceFilter(typeof(Employee_ValidationActionFilter))]
    public async Task<IActionResult> CreateNewEmployee([FromBody] Employee employee)
    {
        _repo.Insert(employee);
        var data = await _repo.GetAllAsync();
        if (data is not null)
        {
            return Ok(data);
        }
        return BadRequest("New user can't be created.");
    }

    [HttpDelete]
    [Route("removeEmployee")]
    public async Task<IActionResult> RemoveEmployee([FromQuery] string empId)
    {
        _repo.Delete(empId);
        var data = await _repo.GetAllAsync();
        return Ok(data);
    }

    [HttpPut]
    [Route("updateEmployee")]
    public Task UpdateEmployee([FromQuery] string empId, [FromBody] Employee newEmployee)
    {
        _repo.Update(empId, newEmployee);
        return Task.FromResult("Db is updated");
    }

}