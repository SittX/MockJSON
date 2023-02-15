using Core.Models;
using WebAPI.Interfaces;

namespace WebAPI.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _empRepo;

    public EmployeeService(IEmployeeRepository empRepo)
    {
        _empRepo = empRepo;
    }

    public Task<IList<Employee>> GetAllAsync()
    {
        return _empRepo.GetAllAsync();
    }

    public Task<Employee> FindAsync(string id)
    {
        return _empRepo.FindAsync(id);
    }

    public void Insert(Employee employee)
    {
        if (employee.Equals(null))
        {
            return;
        }
        _empRepo.Insert(employee);
    }

    public Task<IList<Employee>> UpdateAsync(string empId, Employee employee)
    {
        if (empId.Equals(null) && employee.Equals(null))
        {
            return _empRepo.GetAllAsync();
        }
        
        return _empRepo.UpdateAsync(empId, employee);
    }

    public void Delete(string empId)
    {
        if (empId.Equals(null))
        {
            return;
        }
        
        _empRepo.Delete(empId);
        
    }
}