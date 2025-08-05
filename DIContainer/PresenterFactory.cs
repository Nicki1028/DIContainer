using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DIContainer
{
    public class PresenterFactory
    {
        IServiceProvider provider;
        public PresenterFactory(IServiceProvider provider) {
            this.provider = provider;   
        }
        public TPresenter Create<TPresenter, TView>(TView view)
        {
            Type type = typeof(TPresenter);
            ServiceDescriptor serviceDescriptor = NickiCollection.TypeServiceDescriptorDict[type].Last();
            ConstructorInfo bestConstructor = serviceDescriptor.ImplementationType.GetConstructors()
            .OrderByDescending(c => c.GetParameters().Length)
            .FirstOrDefault();

            if (bestConstructor == null)
                throw new InvalidOperationException("No constructor found for type " + type.FullName);

            var parameters = bestConstructor.GetParameters()
                .Select(x => x.ParameterType.IsInstanceOfType(view)? view : provider.GetService(x.ParameterType))
                .ToArray();
           
            TPresenter presenter = (TPresenter)Activator.CreateInstance(serviceDescriptor.ImplementationType, parameters);
            return presenter;           
        }
    }
}
