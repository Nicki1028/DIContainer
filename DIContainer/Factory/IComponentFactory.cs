using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIContainer
{
    public interface IComponentFactory
    {
        T2 CreateComponent<T, T2>() where T2 : T;
    }
}
