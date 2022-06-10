namespace WebAPI.Interfaces;
public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> FindAsync(string id);
    void Update(string id, T updatedItem);
    void Insert(T newItem);
    void Delete(string id);
}