using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Collections;
using System.Web.Services.Description;
using System.Runtime.CompilerServices;

namespace DIContainer
{
    public class ServiceProviderbyNicki:IServiceProvider
    {
        public Dictionary<Type, ServiceDescriptor> _typeServiceDescriptorDict;
        public Dictionary<Type, object> _singletoninstance = new Dictionary<Type, object>();
        public ServiceProviderbyNicki(Dictionary<Type, ServiceDescriptor> typeServiceDescriptorDict)
        {
            _typeServiceDescriptorDict = typeServiceDescriptorDict;
        }


        public object GetService(Type serviceType)
        {
            _typeServiceDescriptorDict.TryGetValue(serviceType, out ServiceDescriptor descriptor);
            if (descriptor == null)
                throw new Exception("容器中找不到對應的類型");

            object Instance = null;
            if (descriptor.Lifetime == ServiceLifetime.Transient & descriptor.ImplementationFactory != null)
            {
                Instance = descriptor.ImplementationFactory.Invoke(this);

            }
            if (descriptor.Lifetime == ServiceLifetime.Transient & descriptor.ImplementationFactory == null)
            {
                Instance = Activator.CreateInstance(descriptor.ImplementationType);
            }

            if (descriptor.Lifetime == ServiceLifetime.Singleton)
            {

                if (!_singletoninstance.TryGetValue(serviceType, out Instance))
                {
                    if (descriptor.ImplementationFactory != null)
                    {
                        Instance = descriptor.ImplementationFactory.Invoke(this);
                        _singletoninstance[serviceType] = Instance;
                    }
                    else
                    {
                        Instance = Activator.CreateInstance(descriptor.ServiceType);
                        _singletoninstance[serviceType] = Instance;
                    }

                }                                         
            }
            return Instance;
        }
    }
}
