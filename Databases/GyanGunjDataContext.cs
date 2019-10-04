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
                string DBType = ConfigurationManager.AppSettings.Get("Database");
                string Provider = ConfigurationManager.AppSettings.Get("Provider");
                bool IntegratedSecurity = bool.Parse(ConfigurationManager.AppSettings.Get("IntegratedSecurity"));

                if (string.IsNullOrEmpty(DBType) || string.IsNullOrEmpty(Provider))
                    throw new Exception("DBType or Provider not set");

                string ConString = "";

                if (DBType == "SQL")
                {
                    if (IntegratedSecurity)
                        ConString = @"Data Source=" + Provider + ";Initial Catalog=GyanGunj;Integrated Security=True";
                    else
                    {
                        string Username = ConfigurationManager.AppSettings.Get("Username");
                        string Password = ConfigurationManager.AppSettings.Get("Password");
                        ConString = @"Data Source=" + Provider + ";Initial Catalog=GyanGunj;User Id=" + Username + ";Password=" + Password + ";";
                    }
                }

                return ConString;
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
                .Where(type=> type.BaseType != null &&type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>))
                .Select(type=>Activator.CreateInstance(type)).ToArray();

            foreach (dynamic Instance in Instances)
                modelBuilder.Configurations.Add(Instance);

            base.OnModelCreating(modelBuilder);
        }
    }
}
