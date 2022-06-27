namespace WebAPI.Interfaces;
public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> FindAsync(string id);
    Task Update(string id, T updatedItem);
    Task Insert(T newItem);
    void Delete(string id);
}