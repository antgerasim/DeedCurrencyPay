using System.Collections.Generic;
using System.Reflection;

namespace DeedCurrencyPay.Domain.Common
{
    public interface IValueObject<T> where T : IValueObject<T>
    {
        bool Equals(object obj);
        bool Equals(T other);
        int GetHashCode();
        IEnumerable<FieldInfo> GetFields(object obj);
    }
}