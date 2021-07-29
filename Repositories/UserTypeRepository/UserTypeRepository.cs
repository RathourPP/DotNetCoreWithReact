using DummyProjectApi.BusinessModel.Model;
using DummyProjectApi.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DummyProjectApi.Repositories.UserTypeRepository
{
    public class UserTypeRepository : IUserTypeRepository
    {
        private readonly ApplicationDbContext _Context;

        /// <summary>
        /// Resolving Dependency In Constructor
        /// </summary>
        /// <param name="Context"></param>
        public UserTypeRepository(ApplicationDbContext Context)
        {
            _Context = Context;
        }
        public async Task<UserType> Delete(int id)
        {
            var result = await _Context.TypeOfUser.Where(x => x.Id == id && x.IsActive == true).FirstOrDefaultAsync();
            if (result != null)
            {
                result.IsActive = false;
                await _Context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IEnumerable<UserType>> GetAll()
        {
            return await _Context.TypeOfUser.Where(x => x.IsActive == true).ToListAsync();
        }

        public async Task<UserType> GetById(int id)
        {
            return await _Context.TypeOfUser.Where(x => x.Id == id && x.IsActive == true).FirstOrDefaultAsync();
        }

        public async Task<UserType> Save(UserType userType)
        {
            var result = await _Context.TypeOfUser.AddAsync(userType);
            await _Context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<UserType>> Search(string name)
        {
            IQueryable<UserType> query = _Context.TypeOfUser;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }
            return await query.ToListAsync();
        }

        public async Task<UserType> Update(UserType userType)
        {
            var result = await _Context.TypeOfUser.FirstOrDefaultAsync(x => x.Id == userType.Id);
            if (result != null)
            {
               
                result.Name = userType.Name;
                result.User = userType.User;
                result.UpdatedDate = DateTime.UtcNow;
                await _Context.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
