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
        public async Task<IActionResult> GetAllEmployeesAsync()
        {
            var employees = await _repo.GetAllEmployees();
            return Ok(employees);
        }

        [HttpGet]
        [Route("findEmployee")]
        public async Task<IActionResult> GetById([FromQuery] int id)
        {
            var employee = await _repo.SearchEmployee(id);
            if (employee is not null)
            {
                return Ok(employee);
            }
            return BadRequest("User with the given Id cannot be found!");
        }

        [HttpPost]
        [Route("newEmployee")]
        // [ServiceFilter(typeof(Employee_ValidationActionFilter))]
        public IActionResult InsertNewEmployee([FromBody] EmployeeV2 employee)
        {
            _repo.Insert(employee);
            return Ok("Ok");

        }

        [HttpDelete]
        [Route("removeEmployee")]
        public Task RemoveEmployee([FromQuery] int id)
        {
            _repo.Delete(id);
            return Task.CompletedTask;
        }

        [HttpPut]
        [Route("updateEmployee")]
        public Task UpdateEmployee([FromQuery] int id, [FromBody] EmployeeV2 newEmployee)
        {
            _repo.UpdateEmployee(id, newEmployee);
            return Task.CompletedTask;
        }

    }
}