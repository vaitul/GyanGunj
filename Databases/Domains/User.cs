using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databases.Domains
{
    public class User : BaseEntity
    {
        public String Name { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
    }
}
