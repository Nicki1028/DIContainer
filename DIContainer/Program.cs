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
        static void Main(string[] args)
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.
            ServiseCollection serviseCollection = new ServiseCollection();
            serviseCollection.Add<Car, BMW>(EnumAddType.Transient, configure =>
            {
                configure.Id = 1;


            });
            //serviseCollection.Add<Car>(EnumAddType.Transient, () =>
            //{
            //    BMW bMW = new BMW();
            //    bMW.Id = 2;
            //    return bMW;
            //});

            Car BMW = (Car)serviseCollection.GetData<Car>();
            
            Console.WriteLine(BMW.Id);
            BMW.ShowInfo();
            Console.ReadKey();
        }
    }
}
