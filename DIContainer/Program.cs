using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SqlServer.Server;
using NLog.Extensions.Logging;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Description;
using System.Xml.Schema;

namespace DIContainer
{
    internal class Program
    {
        private static IServiceProvider provider;

        static void Main(string[] args)
        {
            //ServiceCollection serviseCollection = new ServiceCollection();
            //serviseCollection.Add<Car, BMW>(EnumAddType.Transient, configure =>
            //{
            //    configure.Id = 1;


            //});
            ////serviseCollection.Add<Car>(EnumAddType.Transient, () =>
            ////{
            ////    BMW bMW = new BMW();
            ////    bMW.Id = 2;
            ////    return bMW;
            ////});

            //Car BMW = (Car)serviseCollection.GetData<Car>();

            //Console.WriteLine(BMW.Id);
            //BMW.ShowInfo();
            //Console.ReadKey();

            //ServiceCollection serviceCollection = new ServiceCollection();

            //使用Activator.CreateInstance創建建構元有一層其他類別

            var config = CreateConfig();
            NickiService service = new NickiService();
            service.AddLogging(x =>
            {
                x.ClearProviders();
                //設定 logging 的 minmum level 為 trace
                x.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                //使用 NLog 作為 logging provider
                x.AddNLog(config);
            });

            service.Add<Cartype>(ServiceLifetime.Singleton);
            provider = service.BuildServiceProvider();
            ILogger logger = provider.GetService<ILogger<Cartype>>();
            logger.Log(LogLevel.Information, "DI注入");
            Cartype mainForm = provider.GetService<Cartype>();


            Console.WriteLine("執行結束");
          
            Console.ReadKey();
        }

        private static IConfiguration CreateConfig()
        {

            var config = new ConfigurationBuilder()
                         .Build();
            return config;
        }
        //public static T CreateInstance<T>()
        //{
        //    Type t = typeof(T);
        //    List<object> instancelist = new List<object>();
        //    var constructors = t.GetConstructors();

        //    MethodInfo method = typeof(Program).GetMethod("CreateInstance");

        //    foreach (var constructor in constructors.OrderByDescending(x => x.GetParameters().Length))
        //    {
        //        try
        //        {
        //            foreach (var para in constructor.GetParameters())
        //            {
        //                MethodInfo methodgeneric = method.MakeGenericMethod(para.ParameterType);
        //                instancelist.Add(methodgeneric.Invoke(null, null));
        //            }
        //            return (T)Activator.CreateInstance(t, instancelist.ToArray());

        //        }
        //        catch { continue; }                             
        //    }
        //    return default(T);
        //}
    }
}
