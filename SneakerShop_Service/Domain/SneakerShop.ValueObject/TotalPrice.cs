using SneakerShop.ValueObjects.Base;
using SneakerShop.ValueObjects.Validators;

namespace SneakerShop.ValueObjects;

public class TotalPrice(decimal amount) : ValueObject<decimal>(new TotalPriceValidator(), amount)
{
    public static TotalPrice operator +(TotalPrice a, TotalPrice b) => new(a.Value + b.Value);
    public static TotalPrice operator +(TotalPrice a, Price b) => new(a.Value + b.Value);
    public static TotalPrice operator -(TotalPrice a, TotalPrice b) => new(a.Value - b.Value);
    public static bool operator <(TotalPrice a, TotalPrice b) => a.Value < b.Value;
    public static bool operator >(TotalPrice a, TotalPrice b) => a.Value > b.Value;
    public static bool operator <=(TotalPrice a, TotalPrice b) => a.Value <= b.Value;
    public static bool operator >=(TotalPrice a, TotalPrice b) => a.Value >= b.Value;
}