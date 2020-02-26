using System;
using System.Collections.Generic;

namespace DeedCurrencyPay.Domain.Common
{
    public interface IValueObjectCollection<TValueobject> : IEnumerable<TValueobject>, IEquatable<IValueObjectCollection<TValueobject>>
        where TValueobject : IValueObject<TValueobject>
    {
        int Count { get; }

        IValueObjectCollection<TValueobject> AddImmutable(TValueobject item);

        IValueObjectCollection<TValueobject> AddRange(IValueObjectCollection<TValueobject> collection);

        bool Contains(TValueobject item);
     
        TValueobject ElementAt(int index);

        bool Equals(object obj);

        bool Equals(TValueobject other);
        new IEnumerator<TValueobject> GetEnumerator();

        int GetHashCode();

        IValueObjectCollection<TValueobject> RemoveImmutable(TValueobject item);

        IValueObjectCollection<TValueobject> Skip(int count);

        IValueObjectCollection<TValueobject> Take(int count);
        TValueobject[] ToArray();
    }
}