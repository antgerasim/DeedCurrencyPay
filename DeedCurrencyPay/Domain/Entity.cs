using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace DeedCurrencyPay.Domain
{
    public abstract class Entity<T> where T : Entity<T>
    {
        public virtual long _Id { get; protected set; }

        protected virtual object Actual => this;

        /*
        public bool Equals([AllowNull] T obj)
        {
            return this.Equals(obj as T);
        }
        */
        public override bool Equals(object obj)
        {
            var other = obj as Entity<T>;

            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (Actual.GetType() != other.Actual.GetType())
                return false;

            if (_Id == 0 || other._Id == 0)
                return false;

            return _Id == other._Id;
        }

        public static bool operator ==(Entity<T> a, Entity<T> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity<T> a, Entity<T> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (Actual.GetType().ToString() + _Id).GetHashCode();
        }


    }
}
