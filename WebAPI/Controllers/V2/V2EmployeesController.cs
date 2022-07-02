using Core.Models;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Interfaces;
using WebAPI.Interfaces.V2;

namespace WebAPI.Controllers.V2
{
    [ApiController]
    [Route("/api/[controller]")]
    public class V2EmployeesController : ControllerBase
    {
        private IEmployeeRepositoryV2 _repo;
        public V2EmployeesController(IEmployeeRepositoryV2 repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _repo.GetAllEmployees();
            return Ok(employees);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetEmployeeById([FromRoute] int id)
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
        public IActionResult CreateNewEmployee([FromBody] EmployeeV2 employee)
        {
            var data = _repo.CreateEmployee(employee);
            if (data is not null)
            {
                return Ok(data);
            }
            else
            {
                return Ok("New user has been created.");
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public Task RemoveEmployee([FromRoute] int id)
        {
            _repo.RemoveEmployee(id);
            return Task.CompletedTask;
        }

        [HttpPut]
        [Route("{id}")]
        public Task UpdateEmployee([FromRoute] int id, [FromBody] EmployeeV2 newEmployee)
        {
            _repo.UpdateEmployee(id, newEmployee);
            return Task.CompletedTask;
        }

    }
}