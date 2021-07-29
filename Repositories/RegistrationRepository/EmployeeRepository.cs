using DummyProjectApi.BusinessModel.Model;
using DummyProjectApi.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DummyProjectApi.Repositories.RegistrationRepository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _Context;
        /// <summary>
        /// Resolving Dependency In Constructor
        /// </summary>
        /// <param name="Context"></param>
        public EmployeeRepository(ApplicationDbContext Context)
        {
            _Context = Context;
        }

        public async Task<Registration> Delete(int id)
        {
            var result = await _Context.EmployeeBasicInformation.Where(x => x.Id == id && x.IsActive == true).FirstOrDefaultAsync();
            if(result!=null)
            {
                result.IsActive = false;
                await _Context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Registration> GetById(int id)
        {
            return await _Context.EmployeeBasicInformation.Where(x => x.Id == id && x.IsActive == true).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Registration>> GetAll()
        {
            return await _Context.EmployeeBasicInformation.Where(x => x.IsActive == true).ToListAsync();
        }

        public async Task<Registration> Save(Registration employee)
        {
            var result = await _Context.EmployeeBasicInformation.AddAsync(employee);
            await _Context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<Registration>> Search(string name)
        {
            IQueryable<Registration> query = _Context.EmployeeBasicInformation;
            if(!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }
            return await query.ToListAsync();
        }

        public async Task<Registration> Update(Registration employee)
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
