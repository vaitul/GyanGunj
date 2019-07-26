using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databases.Interfaces
{
    public partial interface IRepository<T> where T : BaseEntity
    {
        string TableName { get; }

        T GetById(object id);

        void Insert(T entity);
        void Insert(IEnumerable<T> entities);

        void Update(T entity);
        void Update(IEnumerable<T> entities);

        void Delete(T entity);
        void Delete(IEnumerable<T> entities);

        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();

        IQueryable<T> Table { get; }
        IQueryable<T> TableNoTracking { get; }
        IQueryable<T> View { get; }
        IQueryable<T> ViewNoTracking { get; }
        ObservableCollection<T> All { get; }

        DbContext Context { get; }
        //ObjectContext ObjectContext { get; }

        //IEnumerable<dynamic> Execute(string sql, params object[] parameters);
        //DataTable ExecuteDataTable(string criteria = null, params object[] parameters);

        //int UpdateDataTable(DataTable table);

    }
}
