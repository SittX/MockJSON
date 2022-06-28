using Core.Interfaces;
using Core.Models;
using Newtonsoft.Json;

namespace Core.DataAccess
{
    public class EmployeeDataAccess : IEmployeeDataAccess
    {
        private string path = "../Core/Data/employeeData.json";

        public async Task<IEnumerable<Employee>> LoadData()
        {
            var data = await File.ReadAllTextAsync(path);
            var result = JsonConvert.DeserializeObject<List<Employee>>(data);
            if (result is not null)
            {
                return result;
            }
            return Enumerable.Empty<Employee>();
        }

        public async Task<Employee> Search(string id)
        {
            var data = await File.ReadAllTextAsync(path);
            var employees = JsonConvert.DeserializeObject<List<Employee>>(data);
            if (employees is not null)
            {
                var emp = employees.Where(e => e.EmployeeId == id).FirstOrDefault();
                if (emp is not null)
                {
                    return emp;
                }
            }
            return new Employee();
        }

    }
}