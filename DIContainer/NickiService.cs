using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Windows.Forms;
using DIContainer.Attributes;
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
            AddSingleton<PresenterFactory>();
        }

        public void AddSingleton<T, T2>() where T : class where T2 : class, T
        {
            Add<T, T2>(ServiceLifetime.Singleton);
        }
        public void AddSingleton<T>() where T : class
        {
            Add<T>(ServiceLifetime.Singleton);
        }
        public void AddTransient<T>(Func<IServiceProvider, T> factory) where T : class
        {
            Add<T>(ServiceLifetime.Transient, factory);
        }
        public void AddTransient<T, T2>() where T : class where T2 : class, T
        {
            Add<T, T2>(ServiceLifetime.Transient);
        }
        public void AddTransient<T>() where T : class
        {
            Add<T>(ServiceLifetime.Transient);
        }
        public void AddSingleton<T>(Func<IServiceProvider, T> factory) where T : class
        {
            Add<T>(ServiceLifetime.Transient, factory);
        }


        public void Add<T, T2>(ServiceLifetime serviceLifetime) where T : class where T2 : class
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
        //public void Add<T, T2>(ServiceLifetime serviceLifetime, Action<T> configure = null) where T : class where T2 : class
        //{

        //    switch (serviceLifetime)
        //    {
        //        case ServiceLifetime.Singleton:
        //            //var instance = new T2();                  
        //            //configure?.Invoke(instance);
        //            //saveData.configure = configure;
        //            //saveData.AddType = EnumAddType.Singleton;
        //            //saveData.DelegateType = configure != null ? EnumDelegateType.Action: EnumDelegateType.None;                  
        //            //saveData.data = instance;
        //            Collection.Add(new ServiceDescriptor(typeof(T), serviceProvider =>
        //            {
        //                var instance = new T2(); 
        //                configure?.Invoke(instance); 
        //                return instance; 
        //            }, ServiceLifetime.Singleton));

        //            break;
        //        case ServiceLifetime.Transient:
        //            //saveData.AddType = EnumAddType.Transient;
        //            //saveData.DelegateType = configure != null ? EnumDelegateType.Action : EnumDelegateType.None;
        //            //saveData.configure = configure;
        //            //saveData.data = null;
        //            //saveData.datatemp = new T2();
        //            Collection.Add(new ServiceDescriptor(typeof(T), serviceProvider =>
        //            {
        //                var instance = new T2();
        //                configure?.Invoke(instance);
        //                return instance;
        //            }, ServiceLifetime.Transient));
        //            break;
        //    }
        //}
        public void Add<T>(ServiceLifetime serviceLifetime, Func<IServiceProvider, T> factory) where T : class
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
            IServiceProvider provider = new ServiceProviderbyNicki(NickiCollection.TypeServiceDescriptorDict);
            NickiCollection.TypeServiceDescriptorDict[typeof(IServiceProvider)] = new List<ServiceDescriptor>() { new ServiceDescriptor(typeof(IServiceProvider), provider) };
            return provider;
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
            if (!NickiCollection.TypeServiceDescriptorDict.ContainsKey(item.ServiceType))
            {
                NickiCollection.TypeServiceDescriptorDict[item.ServiceType] = new List<ServiceDescriptor>() { item };

            }
            else
            {
                NickiCollection.TypeServiceDescriptorDict[item.ServiceType].Add(item);
            }
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
        public void AutoRegister(IEnumerable<System.Reflection.TypeInfo> types)
        {
            foreach (var type in types)
            {
                if (type.CustomAttributes.Any(x => x.AttributeType != typeof(Signleton) &&  x.AttributeType != typeof(Transient)))
                {
                    continue;
                }



                if (type.CustomAttributes.Any(x => x.AttributeType == typeof(Signleton)))
                {
                    if (type.BaseType != null && type.BaseType != typeof(Object))
                    {
                        Collection.Add(new Microsoft.Extensions.DependencyInjection.ServiceDescriptor(type.BaseType, type, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton));
                    }
                    else if (type.ImplementedInterfaces != null && type.ImplementedInterfaces.Any())
                    {
                        foreach (var data in type.ImplementedInterfaces)
                        {

                            Collection.Add(new Microsoft.Extensions.DependencyInjection.ServiceDescriptor(data, type, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton));
                        }
                    }
                    else
                    {
                        Collection.Add(new Microsoft.Extensions.DependencyInjection.ServiceDescriptor(type, type, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton));
                    }

                }
                else
                {
                    if (type.BaseType != null && type.BaseType != typeof(Object))
                    {
                        Collection.Add(new Microsoft.Extensions.DependencyInjection.ServiceDescriptor(type.BaseType, type, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient));
                    }
                    else if (type.ImplementedInterfaces != null && type.ImplementedInterfaces.Any())
                    {
                        foreach (var data in type.ImplementedInterfaces)
                        {

                            Collection.Add(new Microsoft.Extensions.DependencyInjection.ServiceDescriptor(data, type, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient));
                        }
                    }
                    else
                    {
                        Collection.Add(new Microsoft.Extensions.DependencyInjection.ServiceDescriptor(type, type, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient));
                    }

                }
            }
        }
        public void AutoRegisterMVPcomponent(IEnumerable<System.Reflection.TypeInfo> types)
        {
            foreach (var type in types)
            {
                if (type.BaseType == typeof(UserControl))
                {

                    if (type.ImplementedInterfaces != null && type.ImplementedInterfaces.Any())
                    {
                        foreach (var data in type.ImplementedInterfaces)
                        {
                            Collection.Add(new Microsoft.Extensions.DependencyInjection.ServiceDescriptor(data, type, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient));
                        }
                    }
                    else
                    {
                        Collection.Add(new Microsoft.Extensions.DependencyInjection.ServiceDescriptor(type, type, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient));
                    }


                    if (types.Any(x => x.Name == type.Name))
                    {
                        int index = type.Name.IndexOf("Component");
                        if (index != -1)
                        {
                            string presenter = type.Name.Substring(0, index) + "Presenter";
                            var presenterType = types.First(x => x.Name == presenter);
                            if (presenterType.ImplementedInterfaces != null)
                            {
                                Collection.Add(new Microsoft.Extensions.DependencyInjection.ServiceDescriptor(presenterType, type, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient));
                            }

                        }
                    }

                }
            }
        }

    }
}
