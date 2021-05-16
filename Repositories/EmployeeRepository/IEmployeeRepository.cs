using DummyProjectApi.BusinessModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DummyProjectApi.Repositories.EmployeeRepository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeBasicInformation>> GetEmployeesBasicInformation();
        Task<EmployeeBasicInformation> GetEmployeeBasicInforamtionOnId(int Id);
        Task<EmployeeBasicInformation> RegisterEmployee(EmployeeBasicInformation employee);
        Task<EmployeeBasicInformation> UpdateBasicInformation(EmployeeBasicInformation employee);
        void DeleteEmployeeBasicInfo(int Id);
    }
}
