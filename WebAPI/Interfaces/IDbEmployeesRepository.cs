using QuickType;

namespace WebAPI;

public interface IDbEmployeesRepository
{
    Task<IEnumerable<DbEmployee>> GetAllAsync();
    Task<DbEmployee> Search(string id);
}