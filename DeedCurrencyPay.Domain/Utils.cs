using System.Collections.Generic;
using System.Reflection;

namespace DeedCurrencyPay.Domain
{
    public static class Utils
    {
        public static new bool Equals(object current, object other)
        {
            if (other == null)
                return false;

            if (current.GetType() != other.GetType())
                return false;

            var fields = GetFields(current);

            foreach (var field in fields)
            {
                var value1 = field.GetValue(other);
                var value2 = field.GetValue(current);

                if (value1 == null)
                {
                    if (value2 != null)
                        return false;
                }
                else if (!value1.Equals(value2))
                    return false;
            }
            return true;
        }

        public static IEnumerable<FieldInfo> GetFields(object obj)
        {
            var type = obj.GetType();
            var fields = new List<FieldInfo>();

            while (type != typeof(object))
            {
                if (type == null) continue;
                fields.AddRange(type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public));

                type = type.BaseType;
            }
            return fields;
        }
    }
}