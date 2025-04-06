using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIContainer
{
    public class NickiCollection : IServiceCollection        
    {
        public static Dictionary<Type, List<ServiceDescriptor>> TypeServiceDescriptorDict;

        public NickiCollection()
        {
            TypeServiceDescriptorDict = new Dictionary<Type, List<ServiceDescriptor>>();
            
        }

        public ServiceDescriptor this[int index] { get => GetDescriptorDict(index); set => throw new NotImplementedException(); }

        public int Count => TypeServiceDescriptorDict.Count;

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(ServiceDescriptor item)
        {
           if (!TypeServiceDescriptorDict.ContainsKey(item.ServiceType))
            {
                TypeServiceDescriptorDict[item.ServiceType] = new List<ServiceDescriptor>() { item};

            }
            else
            {
                TypeServiceDescriptorDict[item.ServiceType].Add(item);
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

        public IEnumerator<ServiceDescriptor> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public int IndexOf(ServiceDescriptor item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, ServiceDescriptor item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(ServiceDescriptor item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        private ServiceDescriptor GetDescriptorDict(int index)
        {
            int i = 0;
            foreach (var item in TypeServiceDescriptorDict)
            {
                if (index == i)
                    return item.Value.Last();
                i++;
            }
            return null;
        }
    }
}
