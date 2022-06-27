using Core.Interfaces;
using Core.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Core.DataAccess;



public class EmployeeDataAccessV2 : IEmployeeDataAccessV2
{
    private IConfiguration _config;

    public EmployeeDataAccessV2(IConfiguration config)
    {
        _config = config;
    }


    public async Task<EmployeeV2?> Search(int id)
    {

        using (var connection = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
        {
            Dictionary<int, EmployeeV2> EmployeeDictionary = new Dictionary<int, EmployeeV2>();

            var data = await connection.QueryAsync<EmployeeV2, CurrentJobV2, PreviousJobV2, EmployeeV2>("spEmployee_Get", (e, c, p) =>
            {
                e.CurrentJob = c;

                EmployeeV2? emp;
                if (!EmployeeDictionary.TryGetValue(e.Id, out emp))
                {
                    emp = e;
                    emp.PreviousJobs = new List<PreviousJobV2>();
                    EmployeeDictionary.Add(emp.Id, emp);
                }

                if (p is not null && emp.PreviousJobs is not null)
                {
                    emp.PreviousJobs.Add(p);
                }

                return emp;

            }, new { id = id }, commandType: System.Data.CommandType.StoredProcedure, splitOn: "Current_job_title,Previous_job_title");
            if (data is not null) { return data.FirstOrDefault(); }

            return new EmployeeV2();
        }
    }

    /* This method will execute as follows 
        1. It will loop line by line of the resulting data table
        2. It will look for whether the current Employee object has been added to the Dictionary or not.
        3. For each row, if the Employee is the same , it will not be added to the Dictionary 
        instead the PreviousJobs are added to the Employee's PreviousJobs List 
    */

    public async Task<IEnumerable<EmployeeV2>> LoadData()
    {
        using (var connection = new MySqlConnection(_config.GetConnectionString("DefaultConnection")))
        {
            // This will store returned data table rows in <Key,Value> pairs 
            var EmployeeDictionary = new Dictionary<int, EmployeeV2>();
            var data = await connection.QueryAsync<EmployeeV2, CurrentJobV2, PreviousJobV2, EmployeeV2>("spEmployee_GetAll",
            (e, c, p) =>
            {
                e.CurrentJob = c;

                EmployeeV2? emp;
                if (!EmployeeDictionary.TryGetValue(e.Id, out emp))
                {
                    emp = e;
                    emp.PreviousJobs = new List<PreviousJobV2>();
                    EmployeeDictionary.Add(emp.Id, emp);
                }

                if (p is not null && emp.PreviousJobs is not null)
                {
                    emp.PreviousJobs.Add(p);
                }

                return emp;
            }, splitOn: "current_job_title,previous_job_title");

            var result = data.Distinct().ToList();
            return result;
        }

    }
}