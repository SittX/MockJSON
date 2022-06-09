using Core.Models;

namespace MockJSONDataAPI.Interfaces
{
    public interface IEmployeesRepository
    {
        Task<IList<Employee>> GetAllAsync();
        Task<Employee>? FindAsync(string id);
        void Insert(Employee employee);
        Task<IList<Employee>> UpdateAsync(string empId, Employee employee);
        void Delete(string empId);

    }
}