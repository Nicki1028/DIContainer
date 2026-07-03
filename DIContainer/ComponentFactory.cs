using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIContainer
{
    public class ComponentFactory : IComponentFactory
    {
        IServiceProvider serviceProvider;
        public ComponentFactory(IServiceProvider serviceProvider) 
        { 
            this.serviceProvider = serviceProvider;
        }
        public T2 CreateComponent<T, T2>() where T2:T
        {
            IEnumerable<T> components = (IEnumerable<T>)serviceProvider.GetService(typeof(IEnumerable<T>));
            T2 component = (T2)components.First(x => x.GetType() == typeof(T2));
            return component;
        }
    }
}
