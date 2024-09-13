using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIContainer
{
    internal class SaveData
    {
        public EnumAddType AddType { get; set; }

        public object data { get; set; }

        public object datatemp { get; set; }

        public EnumDelegateType DelegateType { get; set; }

        public object configure { get; set;}

    }
}
