using Core.Models;

namespace WebAPI;

public interface IDbEmployeesRepository
{
    Task<IEnumerable<DbEmployee>> GetAllAsync();
    Task<DbEmployee> Search(string id);
}