using Databases.Domains;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databases.Mapping
{
    class UserMapping : EntityTypeConfiguration<User>
    {
        public UserMapping()
        {
            this.ToTable("Users");

            this.HasKey(x => x.Id);
            this.Property(x => x.CreatedBy);
            this.Property(x => x.CreatedOn);
            this.Property(x => x.CreatedMacId);
            this.Property(x => x.ModifiedBy);
            this.Property(x => x.ModifiedOn);
            this.Property(x => x.ModifiedMacId);
            this.Property(x => x.Deleted);
            this.Property(x => x.DeletedOn);
            this.Property(x => x.DeletedBy);
            this.Property(x => x.DeletedMacId);
            

            this.Property(x => x.Name);
            this.Property(x => x.Username);
            this.Property(x => x.Password);
        }
    }
}
