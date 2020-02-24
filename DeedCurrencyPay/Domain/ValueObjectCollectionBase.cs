using System;
using System.Collections.Generic;
using System.Linq;

namespace DeedCurrencyPay.Domain
{
    public abstract class ValueObjectCollectionBase<T> : IEquatable<ValueObjectCollection<T>>
        where T : IValueObject<T>
    {
        protected readonly ICollection<T> _Items;

        public abstract int Count { get; }

        protected ValueObjectCollectionBase(ICollection<T> collection)
        {

            if (collection == null)
            {
                throw new ArgumentNullException();
            }

            _Items = collection.ToList();

            if (collection.Count > 0)
            {
                foreach (var item in collection)
                {
                    _Items.Add(item);
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var list = obj as ValueObjectCollection<T>;

            return Equals(list);
        }

        public bool Equals(ValueObjectCollection<T> list)
        {
            if (list.Count != this.Count)
                return false;

            bool same = true;

            foreach (var item in list)
            {
                if (same)
                {
                    same = (null != list.FirstOrDefault(x => x.Equals(item)));
                }
            }

            return same;
        }

        public override int GetHashCode()
        {
            int hc = 0;
            if (_Items != null)
                foreach (var item in _Items)
                {
                    hc ^= item.GetHashCode();
                }
            return hc;
        }
    }
}
