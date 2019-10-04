using Databases;
using Databases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Utilities.Interfaces;

namespace Utilities.Services
{
    public partial class ServiceBase<T> : Iservice<T> where T : BaseEntity
    {
        #region ctor

        public ServiceBase(IRepository<T> repository)
        {
            this._Repository = repository;
            MacId = GetMacAddress();
        }

        #endregion

        #region properties

        private readonly IRepository<T> _Repository;
        private readonly string MacId;
        public virtual string TableName
        {
            get
            {
                return _Repository.TableName;
            }
        }

        #endregion

        #region Methods

        public string GetTableName()
        {
            return TableName.Trim().TrimStart('[').TrimEnd(']');
        }
        public T Find(Int64 Id, bool Updatable = true)
        {
            return _Repository.ViewNoTracking.Where(x => x.Id == Id).FirstOrDefault();
        }
        public IEnumerable<T> FindMany(Expression<Func<T, bool>> expression)
        {
            return _Repository.ViewNoTracking.Where(expression);
        }
        public void Insert(T entity)
        {
            if (entity == null)
                return;
            EntityCreated(entity);
            _Repository.Insert(entity);
        }
        public void Insert(IEnumerable<T> entities)
        {
            if (entities == null)
                return;
            foreach (T entity in entities)
                EntityCreated(entity);
            _Repository.Insert(entities);
        }
        public void Update(T entity)
        {
            if (entity == null)
                return;
            EntityModified(entity);
            _Repository.Update(entity);
        }
        public void Update(IEnumerable<T> entities)
        {
            if (entities == null)
                return;
            foreach (T entity in entities)
                EntityModified(entity);
            _Repository.Update(entities);
        }
        public void Delete(long Id)
        {
            var entity = _Repository.ViewNoTracking.Where(x => x.Id == Id).ToList().FirstOrDefault();
            if (entity == null)
                return;
            EntityDeleted(entity);
            this.Update(entity);
        }
        public void Delete(T entity)
        {
            if (entity == null)
                return;
            EntityDeleted(entity);
            this.Update(entity);
        }
        public void Delete(IEnumerable<T> entities)
        {
            if (entities == null)
                return;
            foreach (T entity in entities)
                EntityDeleted(entity);
            this.Update(entities);
        }
        public IEnumerable<T> GetAll()
        {
            return _Repository.ViewNoTracking;
        }
        private string GetMacAddress()
        {
            return NetworkInterface.GetAllNetworkInterfaces()
                            .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                            .Select(nic => nic.GetPhysicalAddress().ToString())
                            .FirstOrDefault();
        }
        private void EntityModified(T entity)
        {
            entity.ModifiedBy = Globals.CurrentUser.Id;
            entity.ModifiedOn = DateTime.Now;
            entity.ModifiedMacId = MacId;
        }
        private void EntityCreated(T entity)
        {
            entity.CreatedBy = Globals.CurrentUser.Id;
            entity.CreatedOn = DateTime.Now;
            entity.CreatedMacId = MacId;
        }
        private void EntityDeleted(T entity)
        {
            entity.Deleted = true;
            entity.DeletedBy = Globals.CurrentUser.Id;
            entity.DeletedOn = DateTime.Now;
            entity.DeletedMacId = MacId;
        }

        #endregion
    }
}
