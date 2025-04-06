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
using System.Reflection;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;

namespace DIContainer
{
    public class ServiceProviderbyNicki : IServiceProvider
    {
        // TODO:
        // 1.要改成允許放入多個子類別
        // 2.自動註冊(利用Attribute自動註冊) / MVP版本的註冊
        public Dictionary<Type, List<ServiceDescriptor>> _typeServiceDescriptorDict;
        public Dictionary<Type, object> _singletoninstance = new Dictionary<Type, object>();
        public ServiceProviderbyNicki(Dictionary<Type, List<ServiceDescriptor>> typeServiceDescriptorDict)
        {
            _typeServiceDescriptorDict = typeServiceDescriptorDict;
        }
        public object GetService(Type serviceType)
        {
            ServiceDescriptor serviceDescriptor;
            if (serviceType.IsEnumerable())
            {
                var serviceTypeInsideIEnurmerable = serviceType.GetGenericArguments()[0];
                serviceDescriptor = GetServiceDescriptorList(serviceTypeInsideIEnurmerable).Last();

                return GetIEnumerableImplementationInstance(serviceTypeInsideIEnurmerable, serviceDescriptor);
            }

            serviceDescriptor = GetServiceDescriptorList(serviceType).Last();
            return GetImplementationInstance(serviceType, serviceDescriptor);


            //var list = _typeServiceDescriptorDict.ToList();

            //var loggerFactory = this.GetService<ILoggerFactory>();

            //Type genericArg = serviceType.GetGenericArguments()[0];
            //Type loggerType = typeof(Logger<>).MakeGenericType(genericArg);
            //Logger<Cartype> a = new Logger<Cartype>(new LoggerFactory());
            //a.Log(LogLevel.Information, "DI注入");
            ////object a = new Logger<Cartype>((ILoggerFactory)list[5].Value.ImplementationInstance);
            //object obj = Activator.CreateInstance(loggerType);

            //Logger<Cartype> logger = (Logger<Cartype>)Activator.CreateInstance(list[6].Value.ImplementationType.MakeGenericType(new Type[] { typeof(Cartype) }));
            //logger.Log(LogLevel.Information, "DI注入");                    

        }
        private IList CreateGenericList(Type type)
        {
            // 獲取List<>類型
            Type listType = typeof(List<>);

            // 創建List<>類型，並將元素類型設置為type
            Type constructedListType = listType.MakeGenericType(type);

            // 創建List<type>的實例
            return (IList)Activator.CreateInstance(constructedListType);
        }

        public static void AddElementToList(IList list, object element, Type type)
        {
            // 獲取List<>.Add方法
            var method = list.GetType().GetMethod("Add");

            // 將element轉型成type並加入list
            method.Invoke(list, new[] { element });
        }

        private object GetIEnumerableImplementationInstance(Type serviceType, ServiceDescriptor serviceDescriptor)
        {
            if (serviceDescriptor == null)
                return null;

            IList result = CreateGenericList(serviceType);
            var implementationInstance = GetImplementationInstance(serviceType, serviceDescriptor);
            AddElementToList(result, implementationInstance, serviceType);

            return result;
        }
        private object GetImplementationInstance(Type serviceType, ServiceDescriptor descriptor)
        {
            if (descriptor == null)
                return null;

            if (descriptor.ImplementationInstance != null)
                return descriptor.ImplementationInstance;
            object Instance = null;
            if (descriptor.Lifetime == ServiceLifetime.Transient && descriptor.ImplementationFactory != null)
            {
                Instance = descriptor.ImplementationFactory.Invoke(this);

            }
            if (descriptor.Lifetime == ServiceLifetime.Transient && descriptor.ImplementationFactory == null)
            {
                MethodInfo method = typeof(ServiceProviderbyNicki).GetMethod("CreateInstance");
                MethodInfo methodgeneric = method.MakeGenericMethod(descriptor.ImplementationType);
                Instance = methodgeneric.Invoke(this, null);
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
                        MethodInfo method = typeof(ServiceProviderbyNicki).GetMethod("CreateInstance");
                        MethodInfo methodgeneric = method.MakeGenericMethod(descriptor.ImplementationType);
                        Instance = methodgeneric.Invoke(this, null);
                        _singletoninstance[serviceType] = Instance;
                    }
                }
            }
            return Instance;
        }
        private List<ServiceDescriptor> GetServiceDescriptorList(Type serviceType)
        {
            _typeServiceDescriptorDict.TryGetValue(serviceType, out List<ServiceDescriptor> serviceDescriptor);

            if (serviceDescriptor == null && serviceType.IsGenericType)
                serviceDescriptor = GetServiceDescriptorListFromGeneric(serviceType);

            return serviceDescriptor;
        }
        private List<ServiceDescriptor> GetServiceDescriptorListFromGeneric(Type serviceType)
        {
            ServiceDescriptor result;
            var genericTypeDefinition = serviceType.GetGenericTypeDefinition();
            if (_typeServiceDescriptorDict.TryGetValue(genericTypeDefinition, out List<ServiceDescriptor> descriptor))
            {

                result = new ServiceDescriptor(
                        serviceType,
                        descriptor.Last().ImplementationType.MakeGenericType(serviceType.GetGenericArguments()),
                        descriptor.Last().Lifetime);
                descriptor.Add(result);

                return descriptor;
            }
            return null;
        }

        public T CreateInstance<T>()
        {
            Type t = typeof(T);
            var constructors = t.GetConstructors();
            List<object> instancelist = new List<object>();


            foreach (var constructor in constructors.OrderByDescending(x => x.GetParameters().Length))
            {

                bool parmsIsSolve = true;
                var parameters = constructor.GetParameters();
                instancelist.Clear();

                foreach (var para in parameters)
                {
                    object resolvedParam = GetService(para.ParameterType);
                    if (resolvedParam == null)
                    {
                        parmsIsSolve = false;
                        break;
                    }
                    instancelist.Add(resolvedParam);
                }
                if (parmsIsSolve)
                    return (T)Activator.CreateInstance(t, instancelist.ToArray());

            }
            var noParamCtor = constructors.FirstOrDefault(c => c.GetParameters().Length == 0);
            if (noParamCtor != null)
            {
                return (T)Activator.CreateInstance(t);
            }
            return default(T);
        }
    }


    //public T CreateInstance<T>()
    //{
    //    //service.AddTransit<IUser,User>()
    //    //service.AddTransit<IPeople,People>()
    //    //service.AddTransit<IStudent,Student>()


    //    // Student(IUser user)
    //    // User(IPeople)
    //    // People()

    //    Type t = typeof(T);
    //    List<object> instancelist = new List<object>();
    //    var constructors = t.GetConstructors();

    //    foreach (var constructor in constructors.OrderByDescending(x => x.GetParameters().Length)) // 兩個參數的建構元
    //    {
    //        try
    //        {
    //            foreach (var para in constructor.GetParameters()) // 兩個參數 Student(IUser user,IClass classroom)
    //            {                       

    //                Object methodgeneric = GetService(para.ParameterType);
    //                instancelist.Add(methodgeneric);
    //            }
    //            return (T)Activator.CreateInstance(t, instancelist.ToArray());
    //        }
    //        catch (Exception ex)
    //        {
    //            continue;
    //        }
    //    }
    //    return (T)Activator.CreateInstance(t);
    //}


}
