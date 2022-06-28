using Core.Interfaces;
using Core.Models;
using Newtonsoft.Json;

namespace Core.DataAccess
{


    public class EmployeeDataAccess : IEmployeeDataAccess
    {
        private string path = "../Core/Data/employeeData.json";

        public async Task<IEnumerable<Employee>?> LoadData()
        {
            if (File.Exists(path))
            {
                Console.WriteLine("File exists");
            }
            var data = await File.ReadAllTextAsync(path);
            if (data is not null)
            {
                return JsonConvert.DeserializeObject<List<Employee>>(data);
            }
            return Enumerable.Empty<Employee>();
        }

        public async Task<Employee?> Search(string id)
        {
            var data = await File.ReadAllTextAsync(path);
            var employees = JsonConvert.DeserializeObject<List<Employee>>(data);
            if (employees is not null)
            {
                var results = employees.Where(e => e.EmployeeId == id);
                if (results is not null)
                {
                    return results.FirstOrDefault();
                }
            }
            return new Employee();
        }

        // private void PopulateEmployeeList()
        // {
        //     var result = File.ReadAllText(path);
        //     var data = JsonConvert.DeserializeObject<List<Employee>>(result);
        //     if (data != null) _employees = data;
        // }

        // public void Delete(string id)
        // {
        //     _employees = _employees.Where(e => e.EmployeeId != id).ToList();
        // }

        // public async Task<IEnumerable<Employee>> GetAllAsync()
        // {
        //     IEnumerable<Employee> _employee
        //     try
        //     {
        //         // Check if _employee already populated with data else fetch data
        //         if (_employees != null && _employees.GetEnumerator().MoveNext()) return _employees;

        //         var json = await File.ReadAllTextAsync(path);
        //         var result = JsonConvert.DeserializeObject<List<Employee>>(json);

        //         if (result == null)
        //         {
        //             return Enumerable.Empty<Employee>();
        //         }
        //         else
        //         {
        //             _employees = result;
        //             return _employees;
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine(ex.Message);
        //         return Enumerable.Empty<Employee>();
        //     }
        // }
        // public Task<Employee> FindAsync(string id)
        // {
        //     var results = _employees.Where(e => e.EmployeeId == id);
        //     return Task.FromResult(results.FirstOrDefault());
        // }
        // public void Insert(Employee newItem)
        // {
        //     // This is only adding new item to the In Memory object and not to the real Db context
        //     // If this method does not work try below one
        //     // _employees.Append(newItem);

        //     // This one might work well than the upper one
        //     _employees.Append(newItem);
        // }

        // public void Update(string id, Employee updatedItem)
        // {
        //     foreach (var emp in _employees)
        //     {
        //         if (emp.EmployeeId == id)
        //         {
        //             emp.FirstName = updatedItem.FirstName;
        //             emp.LastName = updatedItem.LastName;
        //             emp.Email = updatedItem.Email;
        //             emp.Gender = updatedItem.Gender;
        //             emp.Education = updatedItem.Education;
        //             emp.Job = updatedItem.Job;
        //             emp.PreviousJobs = updatedItem.PreviousJobs;
        //             emp.PhoneNumber = updatedItem.PhoneNumber;
        //         }
        //     }
        // }
    }
}