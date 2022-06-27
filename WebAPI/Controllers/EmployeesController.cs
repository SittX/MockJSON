using Core.Models;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Interfaces;

namespace WebAPI;

[ApiController]
[Route("/api/[controller]")]
public class EmployeesController : ControllerBase
{
    private IEmployeeRepository _repo;
    public EmployeesController(IEmployeeRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        var employees = await _repo.GetEmployees();
        return Ok(employees);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> SearchEmployee(string id)
    {
        var employee = await _repo.SearchEmployee(id);
        if (employee is not null)
        {
            return Ok(employee);
        }
        return BadRequest("User with the given Id cannot be found!");
    }

    [HttpPost]
    // [ServiceFilter(typeof(Employee_ValidationActionFilter))]
    public async Task<IActionResult> CreateNewEmployee([FromBody] Employee employee)
    {
        var data = await _repo.Insert(employee);
        if (data is not null)
        {
            return Ok(data);
        }
        return BadRequest("New user can't be created.");
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveEmployee([FromQuery] string id)
    {
        await _repo.Delete(id);
        var data = await _repo.GetEmployees();
        return Ok(data);
    }

    [HttpPut]
    public Task UpdateEmployee([FromQuery] string id, [FromBody] Employee newEmployee)
    {
        _repo.Update(id, newEmployee);
        return Task.FromResult("Db is updated");
    }

}