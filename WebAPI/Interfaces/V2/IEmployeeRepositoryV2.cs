using Core.Models;

namespace WebAPI.Interfaces.V2
{
    public interface IEmployeeRepositoryV2
    {
        void RemoveEmployee(int id);
        Task<IEnumerable<EmployeeV2>> GetAllEmployees();
        Task<IEnumerable<EmployeeV2>> CreateEmployee(EmployeeV2 emp);
        Task<EmployeeV2> SearchEmployee(int id);
        void UpdateEmployee(int id, EmployeeV2 updatedItem);
    }
}