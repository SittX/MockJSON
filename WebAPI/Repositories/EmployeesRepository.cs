using Core.Models;
using MockJSONDataAPI.Interfaces;
using Newtonsoft.Json;

namespace MockJSONDataAPI.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        private string path = "../Core/Data/employeeData.json";
        private IList<Employee> _employees = new List<Employee>();

        public EmployeesRepository()
        {
            GetAllAsync();
        }

        public void Delete(string empId)
        {
            _employees = _employees.Where(e => e.EmployeeId != empId).ToList();
        }

        public async Task<Employee>? FindAsync(string id)
        {
            await GetAllAsync();
            var result = _employees.Where(e => e.EmployeeId == id).ToList();
            if (result == null)
            {
                return null;
            }

            return result[0];
        }

        public async Task<IList<Employee>>? GetAllAsync()
        {
            if (_employees.Count > 0) return _employees;
            var result = await File.ReadAllTextAsync(path);
            var data = JsonConvert.DeserializeObject<List<Employee>>(result);
            if (data != null)
            {
                _employees = data;
                return _employees;
            }
            return null;
        }

        public void Insert(Employee employee)
        {
            _employees.Add(employee);
        }

        public Task<IList<Employee>> UpdateAsync(string empId, Employee newEmployee)
        {
            foreach (var emp in _employees)
            {
                if (emp.EmployeeId == empId)
                {
                    emp.FirstName = newEmployee.FirstName;
                    emp.LastName = newEmployee.LastName;
                    emp.Email = newEmployee.Email;
                    emp.Gender = newEmployee.Gender;
                    emp.Education = newEmployee.Education;
                    emp.Job = newEmployee.Job;
                    emp.PreviousJobs = newEmployee.PreviousJobs;
                    emp.PhoneNumber = newEmployee.PhoneNumber;
                }
            }

            return Task.FromResult(_employees);
        }
    }
}