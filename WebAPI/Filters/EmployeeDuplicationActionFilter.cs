using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPI.Interfaces;

<<<<<<< HEAD:WebAPI/Filters/Employee_ValidationActionFilter.cs
namespace WebAPI
=======
namespace WebAPI.Filters
>>>>>>> feature:WebAPI/Filters/EmployeeDuplicationActionFilter.cs
{
    public class EmployeeDuplicationActionFilter : ActionFilterAttribute
    {
        private readonly IEmployeeRepository _repo;
<<<<<<< HEAD:WebAPI/Filters/Employee_ValidationActionFilter.cs
        public Employee_ValidationActionFilter(IEmployeeRepository repo)
=======
        public EmployeeDuplicationActionFilter(IEmployeeRepository repo)
>>>>>>> feature:WebAPI/Filters/EmployeeDuplicationActionFilter.cs
        {
            _repo = repo;
        }
        public override async void OnActionExecuting(ActionExecutingContext context)
        {
<<<<<<< HEAD:WebAPI/Filters/Employee_ValidationActionFilter.cs
            IEnumerable<Employee> employees = await _repo.GetEmployees();
            var employee = context.ActionArguments["employee"] as Employee;
=======
            IList<Employee> employees = await _repo.GetAllAsync();
>>>>>>> feature:WebAPI/Filters/EmployeeDuplicationActionFilter.cs

            if (context.ActionArguments["employee"] is Employee employee)
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