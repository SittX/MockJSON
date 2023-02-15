using Core.Models;

namespace WebAPI.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IList<Employee>> GetAllAsync();
        Task<Employee> FindAsync(string id);
        void Insert(Employee employee);
        Task<IList<Employee>> UpdateAsync(string empId, Employee employee);
        void Delete(string empId);

    }
}