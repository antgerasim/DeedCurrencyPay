using System;
using System.Collections.Generic;

namespace DeedCurrencyPay.Domain.Common
{
    public interface IValueObjectCollection<TValueobject> : ICollection<TValueobject>, IEquatable<IValueObjectCollection<TValueobject>> 
        where TValueobject : IValueObject<TValueobject>
    {
        int Count { get; }
        bool IsReadOnly { get; }

        void Add(TValueobject item);
        IValueObjectCollection<TValueobject> AddRange(IEnumerable<TValueobject> collection);
        void Clear();
        bool Contains(TValueobject item);
        void CopyTo(TValueobject[] array, int arrayIndex);
        bool Equals(object obj);
        bool Equals(TValueobject other);
        bool Equals(IValueObjectCollection<TValueobject> list);
        IEnumerator<TValueobject> GetEnumerator();
        int GetHashCode();
    }
}