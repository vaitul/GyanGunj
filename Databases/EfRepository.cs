using Databases.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Databases
{
    public partial class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        public EfRepository(DbContext context)
        {
            this._Context = context;
        }

        private DbContext _Context;
        public DbContext Context
        {
            get
            {
                return _Context as DbContext;
            }
        }

        private IDbSet<T> _Entities;
        public IDbSet<T> Entities
        {
            get
            {
                if (_Entities == null)
                    _Entities = _Context.Set<T>();
                return _Entities;
            }
        }


        public virtual T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        public virtual void Insert(T entity)
        {
            if (entity == null)
                return;
            Context.Entry(entity).State = EntityState.Added;
            this._Context.SaveChanges();
            this.Context.Entry(entity).State = EntityState.Detached;
        }
        public virtual void Insert(IEnumerable<T> entities)
        {
            if (entities == null)
                return;

            foreach (var entity in entities)
                Context.Entry(entity).State = EntityState.Added;

            _Context.SaveChanges();

            foreach (var entity in entities)
                Context.Entry(entity).State = EntityState.Detached;
        }
        public virtual void Update(T entity)
        {
            if (entity == null)
                return;
            var AlreadyAttached = Entities.Local.Where(x => x.Id == entity.Id).FirstOrDefault();
            if (AlreadyAttached == null)
            {
                Context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                var entry = Context.Entry(AlreadyAttached);
                entry.CurrentValues.SetValues(entity);
                entry.State = EntityState.Modified;
            }
            this.Context.SaveChanges();
            if (AlreadyAttached == null)
                Context.Entry(entity).State = EntityState.Detached;
            else
                Context.Entry(AlreadyAttached).State = EntityState.Detached;
        }
        public virtual void Update(IEnumerable<T> entities)
        {
            if (entities == null)
                return;

            foreach (var entity in entities)
            {
                var AlreadyAttached = Entities.Local.Where(x => x.Id == entity.Id).FirstOrDefault();
                if (AlreadyAttached == null)
                {
                    Context.Entry(entity).State = EntityState.Modified;
                }
                else
                {
                    var entry = Context.Entry(AlreadyAttached);
                    entry.CurrentValues.SetValues(entity);
                    entry.State = EntityState.Modified;
                }
                this.Context.SaveChanges();
            }
        }
        public virtual void Delete(T entity)
        {
            if (entity == null)
                return;
            this._Context.SaveChanges();
        }
        public virtual void Delete(IEnumerable<T> entities)
        {
            if (entities == null)
                return;

            foreach (var entity in entities)
            {
                this.Context.Entry(entity).State = EntityState.Modified;
            }
            this.Context.SaveChanges();
        }


        public virtual void BeginTransaction()
        {
            if (this._Context.Database.CurrentTransaction != null)
                this._Context.Database.BeginTransaction();
        }
        public virtual void CommitTransaction()
        {
            if (this._Context.Database.CurrentTransaction != null)
                this._Context.Database.CurrentTransaction.Commit();
        }
        public virtual void RollbackTransaction()
        {
            if (this._Context.Database.CurrentTransaction != null)
                this._Context.Database.CurrentTransaction.Rollback();
        }

        private string GetTableName(DbContext _Context1)
        {
            ObjectContext context = ((IObjectContextAdapter)_Context1).ObjectContext;
            string sql = context.CreateObjectSet<T>().ToTraceString();
            Regex regex = new Regex("FROM (?<table>.*) AS");
            Match match = regex.Match(sql);
            string table = match.Groups["table"].Value.Replace("[dbo].", "").Replace("[", "").Replace("]", "");
            return table;
        }
        public string TableName
        {
            get
            {
                return GetTableName(this.Context);
            }
        }
        public ObservableCollection<T> All
        {
            get
            {
                return this.Entities.Local;
            }
        }

        public IQueryable<T> ViewNoTracking
        {
            get
            {
                return this.Entities.AsNoTracking().Where(x => x.Deleted == false);
            }
        }
        public IQueryable<T> TableNoTracking
        {
            get
            {
                return this.Entities.AsNoTracking();
            }
        }

        public IQueryable<T> View
        {
            get
            {
                return this.Entities.Where(x=>x.Deleted == false);
            }
        }
        public IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

    }
}
