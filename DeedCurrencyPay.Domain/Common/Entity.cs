using DeedCurrencyPay.Domain.Common;

namespace DeedCurrencyPay.Domain.Common
{
    public abstract class Entity<T> : IEntity<T> where T : IEntity<T>
    {
        public virtual long Id { get; protected set; }

        protected virtual object Actual => this;

        public static bool operator !=(Entity<T> a, Entity<T> b)
        {
            return !(a == b);
        }

        public static bool operator ==(Entity<T> a, Entity<T> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Entity<T>;

            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (Actual.GetType() != other.Actual.GetType())
                return false;

            if (Id == 0 || other.Id == 0)
                return false;

            return Id == other.Id;
        }
        public override int GetHashCode()
        {
            return (Actual.GetType().ToString() + Id).GetHashCode();
        }
    }
}