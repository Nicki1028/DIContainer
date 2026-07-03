using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
        public TPresenter Create<TPresenter, TView>(TView view, Type presentertype = null)
        {
            Type type = typeof(TPresenter);

            ServiceDescriptor serviceDescriptor = NickiCollection.TypeServiceDescriptorDict[type].Last(x => presentertype == null ? true : x.ImplementationType == presentertype);

            ConstructorInfo bestConstructor = serviceDescriptor.ImplementationType.GetConstructors()
                    .OrderByDescending(c => c.GetParameters().Length)
                    .FirstOrDefault();

            if (bestConstructor == null)
                throw new InvalidOperationException("No constructor found for type " + type.FullName);

            var parameters = bestConstructor.GetParameters()
                .Select(x =>
                {
                    if (x.ParameterType.IsInstanceOfType(view))
                    {
                        return view;
                    }
                    else
                    {
                        var data = provider.GetService(x.ParameterType);
                        return data;
                    }
                }).ToArray();
           
            TPresenter presenter = (TPresenter)Activator.CreateInstance(serviceDescriptor.ImplementationType, parameters);
            return presenter;           
        }
    }
}
