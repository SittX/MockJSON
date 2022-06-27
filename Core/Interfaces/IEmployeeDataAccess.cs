using Core.Models;

namespace Core.Interfaces;

public interface IEmployeeDataAccess
{
    Task<IEnumerable<Employee>?> LoadData();
    Task<Employee?> Search(string id);
}