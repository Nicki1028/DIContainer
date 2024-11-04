using DIContainer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Test.PresenterFactory;

namespace Test
{
    public class PresenterFactory
    {    
        public TPresenter Create<TPresenter,TView>(TView view)
        {
            Type type = typeof(TPresenter);
            ServiceDescriptor descriptor = NickiCollection.TypeServiceDescriptorDict[type];
            TPresenter presenter = (TPresenter)Activator.CreateInstance(descriptor.ImplementationType, view);
            return presenter;
        }

    }
       
}
