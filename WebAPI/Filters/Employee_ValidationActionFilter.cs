using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MockJSONDataAPI.Interfaces;

namespace MockJSONDataAPI
{

    public class Employee_ValidationActionFilter : ActionFilterAttribute
    {
        private readonly IEmployeesRepository _repo;
        public Employee_ValidationActionFilter(IEmployeesRepository repo)
        {
            _repo = repo;
        }
        public override async void OnActionExecuting(ActionExecutingContext context)
        {
            IList<Employee> employees = await _repo.GetAllAsync();
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