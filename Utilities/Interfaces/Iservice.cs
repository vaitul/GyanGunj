using Databases;
using Databases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Interfaces
{
    public partial interface Iservice<T> where T : BaseEntity
    {
        string GetTableName();
        T Find(Int64 Id, bool Updatable = true);
        IEnumerable<T> FindMany(Expression<Func<T, bool>> expression);
        void Insert(T entity);
        void Insert(IEnumerable<T> entity);
        void Update(T entity);
        void Update(IEnumerable<T> entity);
        void Delete(long Id);
        void Delete(T entity);
        void Delete(IEnumerable<T> entity);
        IEnumerable<T> GetAll();
    }
}
