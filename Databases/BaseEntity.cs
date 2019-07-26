using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databases
{
    public abstract partial class BaseEntity
    {
        ///<summary>
        /// Get Or Set the Entity Identifier
        /// </summary>
        public virtual Int64? Id { get; set; }

        ///<summary>
        /// Get Or Set the Entity CreatedBy
        /// </summary>
        public virtual Int64? CreatedBy { get; set; }

        ///<summary>
        /// Get Or Set the Entity CreatedOn
        /// </summary>
        public virtual DateTime? CreatedOn { get; set; }

        ///<summary>
        /// Get Or Set the Entity CreatedMacId
        /// </summary>
        public virtual string CreatedMacId { get; set; }

        ///<summary>
        /// Get Or Set the Entity Deleted
        /// </summary>
        public virtual bool? Deleted { get; set; }

        ///<summary>
        /// Get Or Set the Entity DeletedBy
        /// </summary>
        public virtual Int64? DeletedBy { get; set; }

        ///<summary>
        /// Get Or Set the Entity DeletedOn
        /// </summary>
        public virtual DateTime? DeletedOn { get; set; }

        ///<summary>
        /// Get Or Set the Entity DeletedMacId
        /// </summary>
        public virtual string DeletedMacId { get; set; }


        //public override bool Equals(object obj)
        //{
        //    return Equals(obj as BaseEntity);
        //}
        //public static bool operator ==(BaseEntity x, BaseEntity y)
        //{
        //    return Equals(x, y);
        //}
        //public static bool operator !=(BaseEntity x, BaseEntity y)
        //{
        //    return x != y;
        //}
    }
}
