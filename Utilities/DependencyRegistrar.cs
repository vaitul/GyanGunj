using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Databases;
using Databases.Interfaces;

namespace Utilities
{
    public partial class DependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
        }
    }
}
