using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIContainer
{
    internal class Toyota : Car
    {
      

        public Toyota() 
        {
            
        }
        public override void ShowInfo()
        {
            Console.WriteLine("I am Toyota");
        }
    }
}
