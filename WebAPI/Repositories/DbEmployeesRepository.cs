using Core.DbAccess;
using Core.Models;
using QuickType;

namespace WebAPI.Repositories
{
    // This is the repository connected with MySQL Db
    public class DbEmployeeRepository : IDbEmployeesRepository
    {

        public void Delete(string empId)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<DbEmployee>> GetAllAsync()
        {
            EmployeeDataAccess da = new EmployeeDataAccess();
            var data = await da.LoadData();
            return data;
        }

        public void Insert(Employee employee)
        {
            throw new NotImplementedException();
        }

        public async Task<DbEmployee> Search(string id)
        {
            EmployeeDataAccess da = new EmployeeDataAccess();
            var data = await da.Search(id);
            return data;
        }

        public Task<IList<Employee>> UpdateAsync(string empId, Employee employee)
        {
            throw new NotImplementedException();
        }

    }
}