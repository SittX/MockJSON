using Core.Models;

namespace Core.Interfaces
{
    public interface IEmployeeDataAccessV2
    {
        Task<IEnumerable<EmployeeV2>> LoadData();
        Task<EmployeeV2?> SearchEmployee(int id);
    }
}