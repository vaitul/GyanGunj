using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Databases
{
    public class GyanGunjDataContext : DbContext
    {
        private static String ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;
            }
        }

        public GyanGunjDataContext()
            :base(ConnectionString)
        {
        }
        public GyanGunjDataContext(string NameOrConnectionString)
            :base(NameOrConnectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var Instances = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type=>type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>))
                .Select(type=>Activator.CreateInstance(type)).ToArray();

            foreach (dynamic Instance in Instances)
                modelBuilder.Configurations.Add(Instance);

            base.OnModelCreating(modelBuilder);
        }
    }
}
