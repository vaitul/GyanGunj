using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Databases;
using Databases.Interfaces;
using Maxen.DI;
using Utilities.Interfaces;
using Utilities.Services;

namespace Utilities
{
    public partial class DependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).LifeStyleScoped();

            builder.RegisterType<GyanGunjDataContext>().As<DbContext>().LifeStyleScoped();
            builder.RegisterType<UserService>().As<IUserService>().LifeStyleScoped();
        }
    }
}
