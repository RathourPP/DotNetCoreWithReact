using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DummyProjectApi.Repositories.RegistrationRepository
{
    public abstract class A
    {
        public abstract void Print();
    }

    public class B : A
    {
        public override  void Print()
        {
            Console.WriteLine("Hello");
        }
    }
}
