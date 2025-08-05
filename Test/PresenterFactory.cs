using DIContainer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            ServiceDescriptor serviceDescriptor = NickiCollection.TypeServiceDescriptorDict[type].Last();
            ConstructorInfo bestConstructor = type.GetConstructors()
            .OrderByDescending(c => c.GetParameters().Length)
            .FirstOrDefault();
            TPresenter presenter = (TPresenter)Activator.CreateInstance(serviceDescriptor.ImplementationType, bestConstructor, view);
            return presenter;
        }

    }
       
}
