using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DeedCurrencyPay.Domain
{
    public class ValueObjectList : ICollection<Money>, IEnumerable<Money>, IEquatable<ValueObjectList>
    {
        private readonly ICollection<Money> _Items;
        public int Count
        {
            get
            {
                {
                    return _Items.Count;
                }
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return _Items.IsReadOnly;
            }
        }

        public ValueObjectList()
        {
            _Items = new List<Money>();
        }

        public ValueObjectList(IEnumerable<Money> collection)
        {
            _Items = new List<Money>();

            if (collection == null)
            {
                throw new ArgumentNullException();
            }

            using (IEnumerator<Money> en = collection.GetEnumerator())
            {
                while (en.MoveNext())
                {
                    _Items.Add(en.Current);
                }
            }
        }

        public void Add(Money item)
        {
            _Items.Add(item);
        }

        bool ICollection<Money>.Remove(Money item)
        {
            return _Items.Remove(item);
        }
        public ValueObjectList AddRange(IEnumerable<Money> collection)
        {
            using (IEnumerator<Money> en = collection.GetEnumerator())
            {
                while (en.MoveNext())
                {
                    _Items.Add(en.Current);
                }
            }
            return new ValueObjectList(_Items);
        }

        public void Clear()
        {
            _Items.Clear();
        }

        public bool Contains(Money item)
        {
            return _Items.Contains(item);
        }

        public void CopyTo(Money[] array, int arrayIndex)
        {
            _Items.CopyTo(array, arrayIndex);
        }



        public IEnumerator<Money> GetEnumerator()
        {
            return _Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var list = obj as ValueObjectList;

            return Equals(list);
        }

        public bool Equals(ValueObjectList list)
        {
            if (list.Count != this.Count)
                return false;
            bool same = true;

            using (IEnumerator<Money> en = list.GetEnumerator())
            {
                while (en.MoveNext())
                {
                    if (same)
                    {
                        same = (null != list.FirstOrDefault(item => item.Equals(en.Current)));
                    }
                }
            }
            return same;
        }

        public override int GetHashCode()
        {
            int hc = 0;
            if (_Items != null || _Items.Count == 0)
                foreach (var p in _Items)
                {
                    hc ^= p.GetHashCode();
                    hc = (hc << 7) | (hc >> (32 - 7)); //rotale hc to the left to swipe over all bits
                }
            return hc;
        }


    }
}
