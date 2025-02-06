namespace ATM.Application.Models.MoneyValueObject;

public class Money
{
    public long Value { get; private set; }

    public Money(long value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Money value can't be less than 0");
        }

        Value = value;
    }
}