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
    public async Task<IActionResult> ReturnAllEmployees()
    {
        var employees = await _repo.GetEmployees();
        if (employees is not null)
        {
            return Ok(employees);
        }
        return BadRequest();
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> ReturnEmployeeById(string id)
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
    public async Task<IActionResult> InsertNewEmployee([FromBody] Employee employee)
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
        _repo.Delete(id);

        // Return the new collection to the user
        var data = await _repo.GetEmployees();
        if (data is not null)
        {
            return Ok(data);

        }
        return BadRequest();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateEmployee([FromQuery] string id, [FromBody] Employee newEmployee)
    {
        _repo.Update(id, newEmployee);
        var data = await _repo.GetEmployees();
        if (data is not null)
        {
            return Ok(data);
        }
        return BadRequest();
    }

}
