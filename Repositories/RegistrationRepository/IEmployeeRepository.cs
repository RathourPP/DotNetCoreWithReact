using DummyProjectApi.BusinessModel.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DummyProjectApi.Repositories.RegistrationRepository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Registration>> Search(string name);
        Task<IEnumerable<Registration>> GetAll();
        Task<Registration> GetById(int Id);
        Task<Registration> Save(Registration employee);
        Task<Registration> Update(Registration employee);
        Task<Registration> Delete(int Id);
    }
}
