using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIContainer
{
    public static class GetServiceExtention
    {
        public static Tparent GetService<Tparent>(this IServiceProvider serviceProvider)
        {
            object result = serviceProvider.GetService(typeof(Tparent));

            return (Tparent)result;
        }
    }
}
