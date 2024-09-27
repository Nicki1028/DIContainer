using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DIContainer
{
    public class NickiService
    {
        public NickiCollection Collection { get; private set; }

        public NickiService()
        {
           Collection = new NickiCollection();
        }      

        public void Add<T, T2>(ServiceLifetime serviceLifetime) where T : class where T2 : class, T, new()
        {
            Type serviceType = typeof(T);
            Type implementationType = typeof(T2);
            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton:
                    Collection.Add(new ServiceDescriptor(serviceType, implementationType, ServiceLifetime.Singleton));
                    break;
                case ServiceLifetime.Transient:
                    Collection.Add(new ServiceDescriptor(serviceType, implementationType, ServiceLifetime.Transient));
                    break;
            }           
        }
        public void Add<T>(ServiceLifetime serviceLifetime) where T : class 
        {
            Type serviceType = typeof(T);
            Type implementationType = typeof(T);
            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton:
                    Collection.Add(new ServiceDescriptor(serviceType, implementationType, ServiceLifetime.Singleton));
                    break;
                case ServiceLifetime.Transient:
                    Collection.Add(new ServiceDescriptor(serviceType, implementationType, ServiceLifetime.Transient));
                    break;
            }
        }
        public void Add<T, T2>(ServiceLifetime serviceLifetime, Action<T> configure = null) where T : class where T2 : class, T, new()
        {
                      
            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton:
                    //var instance = new T2();                  
                    //configure?.Invoke(instance);
                    //saveData.configure = configure;
                    //saveData.AddType = EnumAddType.Singleton;
                    //saveData.DelegateType = configure != null ? EnumDelegateType.Action: EnumDelegateType.None;                  
                    //saveData.data = instance;
                    Collection.Add(new ServiceDescriptor(typeof(T), serviceProvider =>
                    {
                        var instance = new T2(); 
                        configure?.Invoke(instance); 
                        return instance; 
                    }, ServiceLifetime.Singleton));
                    
                    break;
                case ServiceLifetime.Transient:
                    //saveData.AddType = EnumAddType.Transient;
                    //saveData.DelegateType = configure != null ? EnumDelegateType.Action : EnumDelegateType.None;
                    //saveData.configure = configure;
                    //saveData.data = null;
                    //saveData.datatemp = new T2();
                    Collection.Add(new ServiceDescriptor(typeof(T), serviceProvider =>
                    {
                        var instance = new T2();
                        configure?.Invoke(instance);
                        return instance;
                    }, ServiceLifetime.Transient));
                    break;
            }
        }
        public void Add<T>(ServiceLifetime serviceLifetime, Func<IServiceProvider,T> factory) where T : class
        {         
            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton:
                    //saveData.configure = factory;
                    //saveData.data = factory?.Invoke();
                    //saveData.AddType = EnumAddType.Singleton;
                    //saveData.DelegateType = factory != null ? EnumDelegateType.Func :EnumDelegateType.None;
                    Collection.Add(new ServiceDescriptor(typeof(T), factory, ServiceLifetime.Singleton));
                    break;
                    
                case ServiceLifetime.Transient:
                    //saveData.configure = factory;
                    //saveData.AddType = EnumAddType.Transient;
                    //saveData.DelegateType = factory != null ? EnumDelegateType.Func : EnumDelegateType.None;
                    //saveData.data = null;
                    Collection.Add(new ServiceDescriptor(typeof(T), factory, ServiceLifetime.Transient));
                    break;
            }
        }
        public void AddLogging(Action<ILoggingBuilder> configure)
        {
            Collection.AddLogging(configure);
        }
        public IServiceProvider BuildServiceProvider()
        {
            return new ServiceProviderbyNicki(NickiCollection.TypeServiceDescriptorDict);
        }
        public T GetInstance<T>()
        {
            return BuildServiceProvider().GetService<T>();
        }

        public int IndexOf(ServiceDescriptor item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, ServiceDescriptor item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public void Add(ServiceDescriptor item)
        {
           // throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(ServiceDescriptor item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(ServiceDescriptor item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<ServiceDescriptor> GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
