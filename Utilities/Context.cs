using Maxen.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class Context
    {
        private static IMaxenContainer container;
        private static IScope GetScope()
        {
            if (container == null)
            {
                var builder = new ContainerBuilder();
                var dr = new DependencyRegistrar();
                dr.Register(builder);

                container = builder.Build();
            }
            return container;
        }

        public static T Resolve<T>()
        {
            return GetScope().Resolve<T>();
        }
    }
}
