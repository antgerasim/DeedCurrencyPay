using DeedCurrencyPay.Domain.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DeedCurrencyPay.Domain.Common
{
    public class ValueObjectCollection<TValueobject> : IValueObjectCollection<TValueobject>
        where TValueobject : IValueObject<TValueobject>
    {
        protected readonly ICollection<TValueobject> _Items;
        public bool IsReadOnly => _Items.IsReadOnly;

        public int Count => _Items.Count;

        public ValueObjectCollection() : this(new List<TValueobject>())
        {
        }

        public ValueObjectCollection(ICollection<TValueobject> collection)
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


        public bool Equals(TValueobject other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var list = obj as IValueObjectCollection<TValueobject>;

            return Equals(list);
        }

        public bool Equals(IValueObjectCollection<TValueobject> list)
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


        public void Add(TValueobject item)
        {
            _Items.Add(item);
        }

        bool ICollection<TValueobject>.Remove(TValueobject item)
        {
            return _Items.Remove(item);
        }

        public IValueObjectCollection<TValueobject> AddRange(IEnumerable<TValueobject> collection)
        {
            using (IEnumerator<TValueobject> en = collection.GetEnumerator())
            {
                while (en.MoveNext())
                {
                    _Items.Add(en.Current);
                }
            }
            return new ValueObjectCollection<TValueobject>(_Items);
        }



        public void Clear()
        {
            _Items.Clear();
        }

        public bool Contains(TValueobject item)
        {
            return _Items.Contains(item);
        }

        public void CopyTo(TValueobject[] array, int arrayIndex)
        {
            _Items.CopyTo(array, arrayIndex);
        }

        public IEnumerator<TValueobject> GetEnumerator()
        {
            return _Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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