using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace DIContainer
{
    public  class ServiseCollection 
    {
        static Dictionary<string, SaveData> keyValue = new Dictionary<string, SaveData>();
        public ServiceCollection Services { get; set; } = new ServiceCollection();
        public ServiseCollection Add<T, T2>(EnumAddType enumAddType) where T : class where T2 : class, T, new()
        {

            SaveData saveData = new SaveData();
            switch (enumAddType)
            {
                case EnumAddType.Singleton:
                    saveData.AddType = EnumAddType.Singleton;
                    saveData.data = new T2();
                    break;
                case EnumAddType.Transient:
                    saveData.AddType = EnumAddType.Transient;
                    saveData.data = null;
                    saveData.datatemp = new T2();
                    break;
            }
            keyValue.Add(typeof(T).Name, saveData);
            return this;
            
        }
        public ServiseCollection Add<T, T2>(EnumAddType enumAddType, Action<T> configure = null) where T : class where T2 : class, T, new()
        {
                      
            SaveData saveData = new SaveData();
            switch (enumAddType)
            {
                case EnumAddType.Singleton:
                    var instance = new T2();                  
                    configure?.Invoke(instance);
                    saveData.configure = configure;
                    saveData.AddType = EnumAddType.Singleton;
                    saveData.DelegateType = configure != null ? EnumDelegateType.Action: EnumDelegateType.None;                  
                    saveData.data = instance;
                    break;
                case EnumAddType.Transient:
                    saveData.AddType = EnumAddType.Transient;
                    saveData.DelegateType = configure != null ? EnumDelegateType.Action : EnumDelegateType.None;
                    saveData.configure = configure;
                    saveData.data = null;
                    saveData.datatemp = new T2();
                    break;
            }
            keyValue.Add(typeof(T).Name, saveData);
            return this;
        }
        public ServiseCollection Add<T>(EnumAddType enumAddType, Func<T> factory = null) where T : class
        {
            SaveData saveData = new SaveData();
            switch (enumAddType)
            {
                case EnumAddType.Singleton:
                    saveData.configure = factory;
                    saveData.data = factory?.Invoke();
                    saveData.AddType = EnumAddType.Singleton;
                    saveData.DelegateType = factory != null ? EnumDelegateType.Func :EnumDelegateType.None;
                    break;
                case EnumAddType.Transient:
                    saveData.configure = factory;
                    saveData.AddType = EnumAddType.Transient;
                    saveData.DelegateType = factory != null ? EnumDelegateType.Func : EnumDelegateType.None;
                    saveData.data = null;
                    break;
            }
            keyValue.Add(typeof(T).Name, saveData);
            return this;
        }
        public object GetData<T>() where T : class 
        {
            keyValue.TryGetValue(typeof(T).Name, out SaveData saveData);

            if (saveData.AddType == EnumAddType.Transient & saveData.DelegateType == EnumDelegateType.Action) 
            {               
                Action<T> configure = (Action<T>)saveData.configure;
                configure((T)saveData.datatemp);
                return saveData.datatemp;
            }
            if (saveData.AddType == EnumAddType.Transient & saveData.DelegateType == EnumDelegateType.Func)
            {
                Func<T> func = (Func<T>)saveData.configure;
                saveData.data = func.Invoke();
              
            }
            if (saveData.AddType == EnumAddType.Transient & saveData.DelegateType == EnumDelegateType.None) 
            {
                saveData.data = saveData.datatemp;
            }
            return saveData.data;
        }
    }
}
