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
    public async Task<IActionResult> GetAllAsync()
    {
        var data = await _repo.GetAllAsync();
        return Ok(data);
    }

    [HttpGet]
    [Route("findEmployee")]
    public async Task<IActionResult> GetById([FromQuery] string id)
    {
        Console.WriteLine("Id number is  : " + id);
        var employee = await _repo.FindAsync(id);
        if (employee != null)
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
        return Ok(data);
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