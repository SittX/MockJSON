using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPI.Interfaces;

namespace WebAPI
{

    public class Employee_ValidationActionFilter : ActionFilterAttribute
    {
        private readonly IEmployeeRepository _repo;
        public Employee_ValidationActionFilter(IEmployeeRepository repo)
        {
            _repo = repo;
        }
        public override async void OnActionExecuting(ActionExecutingContext context)
        {
            IEnumerable<Employee> employees = await _repo.GetEmployees();
            var employee = context.ActionArguments["employee"] as Employee;

            if (employee is not null)
            {
                foreach (var emp in employees)
                {
                    if (emp.EmployeeId == employee.EmployeeId)
                    {
                        context.ModelState.AddModelError("EmployeeId", "Duplicate employee object");
                        var problemDetails = new ValidationProblemDetails(context.ModelState)
                        {
                            Status = StatusCodes.Status400BadRequest,
                        };
                        context.Result = new BadRequestObjectResult(problemDetails);
                    }
                }
            }
            else
            {
                context.ModelState.AddModelError("employee", "Employee object cannot be empty");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }

        }

    }


}