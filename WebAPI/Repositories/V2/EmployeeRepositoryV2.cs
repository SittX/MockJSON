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

        public Task<EmployeeV2> SearchEmployee(int id)
        {
            var results = _employees.Where(e => e.Id == id).FirstOrDefault();
            if (results is not null)
            {
                return Task.FromResult(results);
            }
            return Task.FromResult(new EmployeeV2());
        }

        public Task<IEnumerable<EmployeeV2>> Insert(EmployeeV2 emp)
        {
            _employees = _employees.Append(emp);
            return Task.FromResult(_employees);
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

        public void Delete(int id)
        {
            _employees = _employees.Where(e => e.Id != id).ToList();
        }

    }
}