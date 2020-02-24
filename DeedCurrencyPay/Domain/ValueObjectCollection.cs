using System.Collections;
using System.Collections.Generic;

namespace DeedCurrencyPay.Domain
{
    public class ValueObjectCollection<T> : ValueObjectCollectionBase<T>, ICollection<T>//, IEnumerable<T>
        where T : IValueObject<T>
    {
        public bool IsReadOnly => base._Items.IsReadOnly;

        public override int Count => base._Items.Count;

        public ValueObjectCollection() : this(new List<T>())
        {
        }

        public ValueObjectCollection(ICollection<T> collection) : base(collection)
        {
        }

        public void Add(T item)
        {
            _Items.Add(item);
        }

        bool ICollection<T>.Remove(T item)
        {
            return _Items.Remove(item);
        }

        public ValueObjectCollection<T> AddRange(IEnumerable<T> collection)
        {
            using (IEnumerator<T> en = collection.GetEnumerator())
            {
                while (en.MoveNext())
                {
                    _Items.Add(en.Current);
                }
            }
            return new ValueObjectCollection<T>(_Items);
        }

        public void Clear()
        {
            _Items.Clear();
        }

        public bool Contains(T item)
        {
            return _Items.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            _Items.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}