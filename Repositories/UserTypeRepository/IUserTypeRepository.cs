using DummyProjectApi.BusinessModel.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DummyProjectApi.Repositories.UserTypeRepository
{
    public interface IUserTypeRepository
    {
        Task<IEnumerable<UserType>> Search(string name);
        Task<IEnumerable<UserType>> GetAll();
        Task<UserType> GetById(int Id);
        Task<UserType> Save(UserType userType);
        Task<UserType> Update(UserType userType);
        Task<UserType> Delete(int Id);
    }
}
