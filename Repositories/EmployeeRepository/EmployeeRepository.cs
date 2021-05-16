using DummyProjectApi.BusinessModel.Model;
using DummyProjectApi.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DummyProjectApi.Repositories.EmployeeRepository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _Context;
        public EmployeeRepository(ApplicationDbContext Context)
        {
            _Context = Context;
        }

        public async Task<EmployeeBasicInformation> DeleteEmployeeBasicInfo(int Id)
        {
            var result = await _Context.EmployeeBasicInformation.Where(x => x.Id == Id && x.IsActive == true).FirstOrDefaultAsync();
            if(result!=null)
            {
                result.IsActive = false;
                await _Context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<EmployeeBasicInformation> GetEmployeeBasicInforamtionOnId(int Id)
        {
            return await _Context.EmployeeBasicInformation.Where(x => x.Id == Id && x.IsActive == false).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<EmployeeBasicInformation>> GetEmployeesBasicInformation()
        {
            return await _Context.EmployeeBasicInformation.Where(x => x.IsActive == true).ToListAsync();
        }

        public async Task<EmployeeBasicInformation> RegisterEmployee(EmployeeBasicInformation employee)
        {
            var result = await _Context.EmployeeBasicInformation.AddAsync(employee);
            await _Context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<EmployeeBasicInformation>> SearchEmployeesBasicInformation(string name)
        {
            IQueryable<EmployeeBasicInformation> query = _Context.EmployeeBasicInformation;
            if(!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }
            return await query.ToListAsync();
        }

        public async Task<EmployeeBasicInformation> UpdateBasicInformation(EmployeeBasicInformation employee)
        {
            var result = await _Context.EmployeeBasicInformation.FirstOrDefaultAsync(x => x.Id == employee.Id);
            if(result!=null)
            {
                result.Email = employee.Email;
                result.Name = employee.Name;
                result.UpdatedDate = DateTime.UtcNow;
                await _Context.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
