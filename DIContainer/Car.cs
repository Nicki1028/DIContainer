using Microsoft.Extensions.Logging;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIContainer
{
    abstract class Car
    {
        public int Id { get; set; }
        public abstract void ShowInfo();

       
        public Car() 
        { 
            
        }
    }
}
