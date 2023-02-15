using Core.Models;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Filters;
using WebAPI.Interfaces;

namespace WebAPI.Controllers;

// [ApiVersion("1.0")]
[ApiController]
[Route("/api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _empService;
    public EmployeesController(IEmployeeService empService)
    {
        _empService = empService;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployeesAsync()
    {
        var employees = await _empService.GetAllAsync();
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
        Employee? employee = await _empService.FindAsync(id);
        if (employee.Equals(null))
        {
            return BadRequest("User with the given Id cannot be found!");
        }
        return Ok(employee);
    }

    [HttpPost]
    [Route("newEmployee")]
    [ServiceFilter(typeof(EmployeeDuplicationActionFilter))]
    public async Task<IActionResult> CreateNewEmployee([FromBody] Employee employee)
    {
        _empService.Insert(employee);
        var data = await _empService.GetAllAsync();
        if (data.Equals(null))
        {
            return BadRequest("New user can't be created. Please try again.");
        }
        return Ok(data);
    }

    [HttpDelete]
    [Route("removeEmployee")]
    public async Task<IActionResult> RemoveEmployee([FromQuery] string empId)
    {
        _empService.Delete(empId);
        var data = await _empService.GetAllAsync();
        return Ok(data);
    }

    [HttpPut]
    [Route("updateEmployee")]
    public async Task<IActionResult> UpdateEmployee([FromQuery] string empId, [FromBody] Employee newEmployee)
    {
        var data = await _empService.UpdateAsync(empId, newEmployee);
        return Ok(data);
    }

}