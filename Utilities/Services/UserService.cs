using Databases.Domains;
using Databases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Interfaces;

namespace Utilities.Services
{
    public class UserService : ServiceBase<User>, IUserService
    {
        public UserService(IRepository<User> repository)
            :base(repository)
        {

        }
    }
}
