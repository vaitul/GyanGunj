using Databases;
using Databases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Services
{
    public partial class ServiceBase<T> where T : BaseEntity
    {
        protected readonly IRepository<T> _Repository;

        #region ctor

        public ServiceBase(IRepository<T> repository)
        {
            this._Repository = repository;
        }

        #endregion

        #region Methods

        public virtual string TableName
        {
            get
            {
                return _Repository.TableName;
            }
        }

        private string GetTableName()
        {
            return TableName.Trim().TrimStart('[').TrimEnd(']');
        }

        public T Find(Int64 Id, bool Updatable = true)
        {
            return _Repository.ViewNoTracking.Where(x => x.Id == Id).FirstOrDefault();
        }

        public IEnumerable<T> FindMany(Expression<Func<T,bool>> expression)
        {
            return _Repository.ViewNoTracking.Where(expression);
        }

        public void Insert(T entity)
        {
            if (entity == null)
                return;

        }
        #endregion
    }
}
