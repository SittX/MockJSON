using Dapper;

using MySql.Data.MySqlClient;

namespace Core.DbAccess
{
    // This class should be Generic 
    public class SQLDataAccess<T>
    {

        private string _connectionString = "server=127.0.0.1;user=root;pwd=kstmysql;database=MockAPI_db";

        public async Task<IEnumerable<T>> LoadData()
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
                return await connection.QueryAsync<T>(query);
            }
        }

        public async Task SaveData<Thing>(T item)
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
                var data = await connection.QueryAsync<T>(query);
            }
        }
    }
}