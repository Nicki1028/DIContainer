using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIContainer
{
    public class PresenterFactory
    {
        public TPresenter Create<TPresenter, TView>(TView view)
        {
            Type type = typeof(TPresenter);
            ServiceDescriptor serviceDescriptor = NickiCollection.TypeServiceDescriptorDict[type];
            TPresenter presenter = (TPresenter)Activator.CreateInstance(serviceDescriptor.ImplementationType, view);
            return presenter;

        }
    }
}
