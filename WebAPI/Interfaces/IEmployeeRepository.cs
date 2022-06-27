using Core.Models;

namespace WebAPI.Interfaces
{
    public interface IEmployeeRepository
    {
        Task Delete(string id);
        Task<IEnumerable<Employee>> GetEmployees();
        Task<IEnumerable<Employee>> Insert(Employee newItem);
        Task<Employee> SearchEmployee(string id);
        void Update(string id, Employee updatedItem);
    }
}