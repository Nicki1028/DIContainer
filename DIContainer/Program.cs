using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
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

            NickiService service = new NickiService();

            //ServiceCollection serviceCollection = new ServiceCollection();

            service.Add<Cartype>(EnumAddType.Singleton);
            service.Add<Car>(EnumAddType.Transient, x =>
            {
                Cartype form1 = x.GetService<Cartype>();
                if (form1.typename == "BMW")
                    return new BMW();
                else if (form1.typename == "Toyota")
                    return new Toyota();
                else
                    return null;
            });
            provider = service.BuildServiceProvider();
            Cartype mainForm = provider.GetService<Cartype>();
            mainForm.typename = "Toyota";
            Car car = provider.GetService<Car>();
            car.ShowInfo();

            Console.ReadKey();
        }
    }
}
