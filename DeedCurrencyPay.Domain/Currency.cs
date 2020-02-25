using System;
using System.Diagnostics.CodeAnalysis;

namespace DeedCurrencyPay.Domain
{
    public struct Currency : IEquatable<Currency>
    {
        public static readonly Currency NONE = new Currency("NONE", 0);
        public static readonly Currency EUR = new Currency("EUR", 1);
        public static readonly Currency USD = new Currency("USD", 2);
        public static readonly Currency RUB = new Currency("RUB", 3);
        public static readonly Currency IDR = new Currency("IDR", 4);

        public string Name { get; }

        public short Index { get; }

        private Currency(string name, short index)
        {
            this.Name = name;
            this.Index = index;
        }

        public bool Equals([AllowNull] Currency other)
        {
            return Name == other.Name && Index == other.Index;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is Currency && Equals((Currency)obj);
        }

        public override int GetHashCode()
        {
            unchecked 
            {
                int hash = 17;
                hash = hash * 23 + (Name != null ? Name.GetHashCode() : 0);
                hash = hash * 23 + Index.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(Currency a, Currency b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Currency a, Currency b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            return $"{Name}";
        }

        public static Currency Parse(string currString)
        {
            if (currString == Currency.EUR.Name)
            {
                return Currency.EUR;
            }
            else if (currString == Currency.USD.Name)
            {
                return Currency.USD;
            }
            else if (currString == Currency.RUB.Name)
            {
                return Currency.RUB;
            }
            else if (currString == Currency.IDR.Name)
            {
                return Currency.IDR;
            }
            return Currency.NONE;
        }

    }
}