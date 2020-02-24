namespace DeedCurrencyPay.Domain
{
    public interface IValueObject<T> where T : IValueObject<T>
    {
        bool Equals(object obj);
        bool Equals(T other);
        int GetHashCode();
    }
}