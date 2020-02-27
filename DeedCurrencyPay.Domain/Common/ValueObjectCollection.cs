using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;

namespace DeedCurrencyPay.Domain.Common
{
    public class ValueObjectCollection<TValueObject> : IValueObjectCollection<TValueObject>
        where TValueObject : IValueObject<TValueObject>
    {
        //todo next: let ValueObjectCollection implement IImmutableCollection with custom object initializer class like
        //https://smellegantcode.wordpress.com/2009/01/29/using-collection-initializers-with-immutable-lists/
        //Todo inherit IImmutableList to IValueObjectCollection

        private readonly IImmutableList<TValueObject> _Items;

        public ValueObjectCollection() : this(new List<TValueObject>())
        {
        }

        public ValueObjectCollection(IEnumerable<TValueObject> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException();
            }
            _Items = collection.ToImmutableList();           
        }

        public int Count => _Items.Count;

        public IValueObjectCollection<TValueObject> AddImmutable(TValueObject item)
        {

            var rslt = _Items.Add(item);
            return new ValueObjectCollection<TValueObject>(rslt);
        }

        public IValueObjectCollection<TValueObject> AddRange(IValueObjectCollection<TValueObject> collection)
        {            
            var resultList = _Items.AddRange(collection.ToImmutableList());
            return new ValueObjectCollection<TValueObject>(resultList);
        }

        public void Clear()
        {
            _Items.Clear();
        }

        public bool Contains(TValueObject item)
        {
            return _Items.Contains(item);
        }

        public TValueObject ElementAt(int index)
        {
            return _Items.ElementAt(index);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var list = obj as IValueObjectCollection<TValueObject>;

            return Equals(list);
        }

        public bool Equals(TValueObject other)
        {
           return  Utils.Equals(this, other);
        }

        public bool Equals(IValueObjectCollection<TValueObject> other)
        {
            if (other.Count != this.Count)
                return false;

            bool same = true;

            foreach (var item in other)
            {
                if (same)
                {
                    same = (null != other.FirstOrDefault(x => x.Equals(item)));
                }
            }
            return same;
        }

        public IEnumerator<TValueObject> GetEnumerator()
        {
            return _Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() //dont touch!
        {
            return _Items.GetEnumerator();
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

        public IValueObjectCollection<TValueObject> RemoveImmutable(TValueObject item)
        {
            _Items.Remove(item);
            return new ValueObjectCollection<TValueObject>(_Items);
        }

        public IValueObjectCollection<TValueObject> Skip(int count)
        {
            return new ValueObjectCollection<TValueObject>(_Items.Skip(count));
        }

        public IValueObjectCollection<TValueObject> Take(int count)
        {
            return new ValueObjectCollection<TValueObject>(_Items.Take(count));
        }

        public TValueObject[] ToArray()
        {
            return _Items.ToArray();
        }
    }
}