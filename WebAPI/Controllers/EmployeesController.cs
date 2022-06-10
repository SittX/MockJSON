using Core.Models;
using Microsoft.AspNetCore.Mvc;
using MockJSONDataAPI.Interfaces;

namespace MockJSONDataAPI;

[ApiVersion("1.0")]
[ApiController]
[Route("/api/[controller]")]
public class EmployeesController : ControllerBase
{
    private IEmployeesRepository _repo;
    public EmployeesController(IEmployeesRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployeesAsync()
    {
        var employees = await _repo.GetAllAsync();
        if (employees.Count > 0)
        {
            return Ok(employees);
        }
        return BadRequest();
    }

    [HttpGet]
    [Route("findEmployee")]
    public async Task<IActionResult> GetById([FromQuery] string id)
    {
        Employee? employee = await _repo.FindAsync(id);
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
    public async Task<IActionResult> UpdateEmployee([FromQuery] string empId, [FromBody] Employee newEmployee)
    {
        var data = await _repo.UpdateAsync(empId, newEmployee);
        return Ok(data);
    }

}