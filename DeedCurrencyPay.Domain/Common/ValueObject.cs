using DeedCurrencyPay.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DeedCurrencyPay.Domain.Common
{
    public abstract class ValueObject<T> : IEquatable<T>, IValueObject<T> where T : ValueObject<T>
    {
        public static bool operator !=(ValueObject<T> x, ValueObject<T> y)
        {
            return !(x == y);
        }

        public static bool operator ==(ValueObject<T> x, ValueObject<T> y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (((object)x == null) || ((object)y == null))
            {
                return false;
            }

            return x.Equals(y);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var other = obj as T;

            return Equals(other);
        }

        public virtual bool Equals(T other)
        {
            return Utils.Equals(this, other);
        }

        public override int GetHashCode()
        {
            var fields = GetFields(this);

            var startValue = 17;
            var multiplier = 59;

            return fields
                .Select(field => field.GetValue(this))
                .Where(value => value != null)
                .Aggregate(
                    startValue,
                        (current, value) => current * multiplier + value.GetHashCode());
        }

        public IEnumerable<FieldInfo> GetFields(object obj)
        {
            return Utils.GetFields(obj);
        }
    }
}