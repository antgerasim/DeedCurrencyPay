using System;
using System.Collections.Generic;

namespace DeedCurrencyPay.Domain.Common
{
    public interface IValueObjectCollection<TValueobject> : ICollection<TValueobject>, IEquatable<IValueObjectCollection<TValueobject>> 
        where TValueobject : IValueObject<TValueobject>
    {
        bool Equals(object obj);
        bool Equals(TValueobject other);
        IValueObjectCollection<TValueobject> AddRange(IEnumerable<TValueobject> collection);
        IValueObjectCollection<TValueobject> AddRange(IValueObjectCollection<TValueobject> collection);
        int GetHashCode();
    }
}