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

    // This method is used to populate _employees collection when the class got instantiated for the first time
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
        // Check if the collection is empty or not
        if (_employees.GetEnumerator().MoveNext())
        {
            return _employees;
        }

        var empList = await _dataAccess.LoadData();
        if (empList is not null)
        {
            return empList;
        }
        else
        {
            return Enumerable.Empty<Employee>();
        }
    }


    public Task<Employee> SearchEmployee(string id)
    {
        var empList = _employees.Where(e => e.EmployeeId == id).FirstOrDefault();
        if (empList is not null)
        {
            return Task.FromResult(empList);
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


    public void Delete(string id)
    {
        _employees = _employees.Where(e => e.EmployeeId != id).ToList();
    }

    public Task<IEnumerable<Employee>> Insert(Employee newItem)
    {
        // Guard clause
        if (newItem is null)
        {
            return Task.FromResult(Enumerable.Empty<Employee>());
        }

        // Create new instances of Job and PreviousJob classes and insert into Employee class
        if (newItem.Job is not null && newItem.PreviousJobs is not null)
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
        else
        {
            return Task.FromResult(Enumerable.Empty<Employee>());
        }
    }
    #endregion
}

