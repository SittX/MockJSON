using Core.Interfaces;
using Dapper;
using MySql.Data.MySqlClient;
using QuickType;

namespace Core.DbAccess;

public class EmployeeDataAccess : IDataAccess<DbEmployee>
{
    private string _connectionString = "server=127.0.0.1;user=root;pwd=kstmysql;database=MockAPI_db";
    public Task<IEnumerable<DbEmployee>> Insert()
    {
        throw new Exception();
    }

    public async Task<IEnumerable<DbEmployee>> LoadData()
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            string query = @"SELECT * FROM employee INNER JOIN job
                            ON job.Employee_Id = employee.Id
                            UNION
                            SELECT * 
                            FROM previous_jobs
                            INNER JOIN employee
                            ON previous_jobs.Employee_Id = employee.Id;";
            connection.Open();
            var result = await connection.QueryAsync<DbEmployee>(query);
            return result;
        }
    }


    public async Task<DbEmployee> Search(string id)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            string query = $"SELECT * FROM employee WHERE employee.Id = {id}";
            connection.Open();
            var result = await connection.QueryAsync<DbEmployee>(query);
            return result.ToList()[0];
        }
    }
}