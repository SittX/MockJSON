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
            PopulateEmpList();
        }
        private void PopulateEmpList()
        {
            var result = File.ReadAllText(path);
            var data = JsonConvert.DeserializeObject<List<Employee>>(result);
            if (data != null) _employees = data;
        }

        #region CRUD methods
        public void Delete(string empId)
        {
            _employees = _employees.Where(e => e.EmployeeId != empId).ToList();
        }

        public Task<Employee> FindAsync(string id)
        {
            var result = _employees.Where(e => e.EmployeeId == id).ToList();
            return Task.FromResult(result[0]);
        }

        public async Task<IList<Employee>> GetAllAsync()
        {
            if (_employees.Count > 0) return _employees;
            try
            {
                var result = await File.ReadAllTextAsync(path);
                var data = JsonConvert.DeserializeObject<List<Employee>>(result);
                if (data is not null)
                    _employees = data;
                return _employees;
            }
            catch (FileNotFoundException fx)
            {
                Console.WriteLine(fx.Message);
                return _employees;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return _employees;
            }
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
        #endregion
    }
}