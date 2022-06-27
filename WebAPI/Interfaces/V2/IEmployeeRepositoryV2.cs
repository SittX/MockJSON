using Core.Models;

namespace WebAPI.Interfaces.V2
{
    public interface IEmployeeRepositoryV2
    {
        Task Delete(int id);
        Task<IEnumerable<EmployeeV2>> GetEmployees();
        Task<IEnumerable<EmployeeV2>> Insert(EmployeeV2 emp);
        Task<EmployeeV2> Search(int id);
        void Update(int id, EmployeeV2 updatedItem);
    }
}