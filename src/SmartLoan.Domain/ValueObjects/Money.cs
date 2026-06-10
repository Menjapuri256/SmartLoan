namespace SmartLoan.Domain.ValueObjects;

public class Money
{
    public decimal Amount {get; private set;}

    public string Currency {get; private set;} = "USD";

    public Money( decimal amount, string currency = "USD")
    {
        Amount = amount;
        Currency = currency;
    }

    public override bool Equals(object? obj)
    {
        if(obj is not Money other) return false;
        return Amount == other.Amount && Currency == other.Currency;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Amount, Currency);
    }

}