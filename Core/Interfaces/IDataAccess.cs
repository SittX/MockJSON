namespace Core.Interfaces;

public interface IDataAccess<T>
{
    Task<IEnumerable<T>> LoadData();
    Task<IEnumerable<T>> Insert();
}