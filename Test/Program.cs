using DIContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Test
{
    internal static class Program
    {

        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            NickiService service = new NickiService();

                      
            //service.Add<PresenterFactory>(Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton); // 註冊 ProductPresenterFactory        
            
            service.Add<IProductPresenter,ProductPresenter>(Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient);

            service.Add<Form, Form1>(Microsoft.Extensions.DependencyInjection.ServiceLifetime.Transient);
            IServiceProvider provider = service.BuildServiceProvider();
            Form form = provider.GetService<Form>();
            Application.Run(form);
        }
    }
}
