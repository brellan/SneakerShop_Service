using SneakerShop.ValueObjects.Base;
using SneakerShop.ValueObjects.Validators;

namespace SneakerShop.ValueObjects;

public class Price(decimal amount) : ValueObject<decimal>(new PriceValidator(), amount)
{
    public static Price operator +(Price a, Price b) => new(a.Value + b.Value);
    public static Price operator -(Price a, Price b) => new(a.Value - b.Value);
    public static Price operator *(Price a, Quantity b) => new(a.Value * b.Value);
    public static Price operator *(Quantity a, Price b) => new(a.Value * b.Value);
    public static bool operator <(Price a, Price b) => a.Value < b.Value;
    public static bool operator >(Price a, Price b) => a.Value > b.Value;
    public static bool operator <=(Price a, Price b) => a.Value <= b.Value;
    public static bool operator >=(Price a, Price b) => a.Value >= b.Value;
}