namespace DeedCurrencyPay.Domain.Common
{
    public interface IEntity<T> where T : IEntity<T>
    {
        long Id { get; }

        bool Equals(object obj);
        int GetHashCode();
    }
}