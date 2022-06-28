using Core.DataAccess;
using Core.Interfaces;
using Core.Models;
using WebAPI.Interfaces;
namespace WebAPI.Repositories;


public class EmployeeRepository : IEmployeeRepository
{
    private IEnumerable<Employee> _employees = new List<Employee>();
    private IEmployeeDataAccess _dataAccess;
    public EmployeeRepository(IEmployeeDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
        PopulateEmployeeList();
    }

    private async void PopulateEmployeeList()
    {
        var data = await _dataAccess.LoadData();
        if (data is not null)
        {
            _employees = data;
        }
    }


    #region CRUD with DataAccess layer
    public async Task<IEnumerable<Employee>> GetEmployees()
    {
        if (_employees.GetEnumerator().MoveNext())
        {
            return _employees;
        }
        var data = await _dataAccess.LoadData();
        if (data is not null)
        {
            return data;
        }
        return Enumerable.Empty<Employee>();
    }


    public Task<Employee> SearchEmployee(string id)
    {
        var results = _employees.Where(e => e.EmployeeId == id);
        if (results is not null)
        {
            return Task.FromResult(results.FirstOrDefault());

        }
        return Task.FromResult(new Employee());
    }


    public void Update(string id, Employee updatedItem)
    {
        foreach (var emp in _employees)
        {
            if (emp.EmployeeId == id)
            {
                emp.FirstName = updatedItem.FirstName;
                emp.LastName = updatedItem.LastName;
                emp.Email = updatedItem.Email;
                emp.Gender = updatedItem.Gender;
                emp.Education = updatedItem.Education;
                emp.Job = updatedItem.Job;
                emp.PreviousJobs = updatedItem.PreviousJobs;
                emp.PhoneNumber = updatedItem.PhoneNumber;
            }
        }
    }


    public Task Delete(string id)
    {
        _employees = _employees.Where(e => e.EmployeeId != id).ToList();
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Employee>> Insert(Employee newItem)
    {
        if (newItem is null)
        {
            return Task.FromResult(Enumerable.Empty<Employee>());
        }

        if (newItem.Job is not null)
        {
            var currentJob = new Job()
            {
                Title = newItem.Job.Title,
                Department = newItem.Job.Department,
                Company = newItem.Job.Company
            };

            var emp = new Employee()
            {
                EmployeeId = newItem.EmployeeId,
                FirstName = newItem.FirstName,
                LastName = newItem.LastName,
                Email = newItem.Email,
                Gender = newItem.Gender,
                PhoneNumber = newItem.PhoneNumber,
                Education = newItem.Education,
                Job = currentJob,
                PreviousJobs = newItem.PreviousJobs
            };
            // Assign a new sequence returned from Append() method to _employee variable 
            _employees = _employees.Append(emp);

            return Task.FromResult(_employees);
        }

        return Task.FromResult(Enumerable.Empty<Employee>());
    }
    #endregion
}

