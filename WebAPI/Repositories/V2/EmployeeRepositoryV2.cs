using Core.Interfaces;
using Core.Models;
using WebAPI.Interfaces;
using WebAPI.Interfaces.V2;

namespace WebAPI.Repositories.V2
{


    public class EmployeeRepositoryV2 : IEmployeeRepositoryV2
    {
        private static IEnumerable<EmployeeV2> _employees = new List<EmployeeV2>();
        private IEmployeeDataAccessV2 _dataAccess;
        public EmployeeRepositoryV2(IEmployeeDataAccessV2 dataAccess)
        {
            _dataAccess = dataAccess;
            Task task = PopulateList();
        }

        // Method to populate the _employee List
        private async Task PopulateList()
        {
            _employees = await GetAllEmployees();
        }

        public async Task<IEnumerable<EmployeeV2>> GetAllEmployees()
        {
            if (_employees.GetEnumerator().MoveNext())
            {
                return _employees;
            }

            var data = await _dataAccess.LoadData();
            if (data is not null)
            {
                return data;
            }
            else
            {
                return Enumerable.Empty<EmployeeV2>();
            }
        }

        public async Task<EmployeeV2> SearchEmployee(int id)
        {
            await PopulateList();
            var results = _employees.Where(e => e.Id == id).FirstOrDefault();
            if (results is not null)
            {
                return results;
            }
            return new EmployeeV2();
        }

        public async Task<IEnumerable<EmployeeV2>> CreateEmployee(EmployeeV2 emp)
        {
            await PopulateList();
            _employees = _employees.Append(emp);
            return _employees;
        }

        public void UpdateEmployee(int id, EmployeeV2 updatedItem)
        {
            foreach (var emp in _employees)
            {
                if (emp.Id == id)
                {
                    emp.FirstName = updatedItem.FirstName;
                    emp.LastName = updatedItem.LastName;
                    emp.Email = updatedItem.Email;
                    emp.Gender = updatedItem.Gender;
                    emp.College = updatedItem.College;
                    emp.Ph_Number = updatedItem.Ph_Number;
                    emp.PreviousJobs = updatedItem.PreviousJobs;
                    if (emp.CurrentJob is not null && updatedItem.CurrentJob is not null)
                    {
                        emp.CurrentJob.Current_job_title = updatedItem.CurrentJob.Current_job_title;
                        emp.CurrentJob.Company = updatedItem.CurrentJob.Company;
                        emp.CurrentJob.Department = updatedItem.CurrentJob.Department;
                    }
                }
            }
        }

        public void RemoveEmployee(int id)
        {
            _employees = _employees.Where(e => e.Id != id).ToList();
        }

    }
}