using Core.Models;
using Newtonsoft.Json;
using WebAPI.Interfaces;
namespace WebAPI.Repositories;

public class EmployeeRepository : IRepository<Employee>
{
    private string path = "../Core/Data/employeeData.json";
    private IEnumerable<Employee> _employees = new List<Employee>();

    public EmployeeRepository()
    {
        PopulateEmployeeList();
    }

    private void PopulateEmployeeList()
    {
        var result = File.ReadAllText(path);
        var data = JsonConvert.DeserializeObject<List<Employee>>(result);
        if (data != null) _employees = data;
    }

    public void Delete(string id)
    {
        _employees = _employees.Where(e => e.EmployeeId != id).ToList();
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        try
        {
            // Check if _employee already populated with data else fetch data
            if (_employees != null && _employees.GetEnumerator().MoveNext()) return _employees;

            var json = await File.ReadAllTextAsync(path);
            var result = JsonConvert.DeserializeObject<List<Employee>>(json);

            if (result == null)
            {
                return Enumerable.Empty<Employee>();
            }
            else
            {
                _employees = result;
                return _employees;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Enumerable.Empty<Employee>();
        }
    }
    public Task<Employee> FindAsync(string id)
    {
        var result = _employees.Where(e => e.EmployeeId == id).ToList();
        return Task.FromResult(result[0]);
    }
    public void Insert(Employee newItem)
    {
        // This is only adding new item to the In Memory object and not to the real Db context
        // If this method does not work try below one
        // _employees.Append(newItem);

        // This one might work well than the upper one
        _employees = _employees.Append(newItem);
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
}