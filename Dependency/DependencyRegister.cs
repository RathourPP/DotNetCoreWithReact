using Autofac;

using DummyProjectApi.Repositories.RegistrationRepository;
using Microsoft.Extensions.Configuration;

namespace DummyProjectApi.Dependency
{
    public class DependencyRegister: Autofac.Module
    {
        #region Will Use Later On In The Project

        private readonly IConfiguration _Configuration;
        public DependencyRegister(IConfiguration Configuration)
        {
            _Configuration = Configuration;
        }

        #endregion
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>().InstancePerLifetimeScope();
        }
    }
}
