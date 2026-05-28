using SneakerShop.ValueObjects.Base;
using SneakerShop.ValueObjects.Validators;

namespace SneakerShop.ValueObjects;

public class Quantity(int value) : ValueObject<int>(new QuantityValidator(), value)
{
    public static Quantity operator +(Quantity a, Quantity b) => new(a.Value + b.Value);
    public static Quantity operator -(Quantity a, Quantity b) => new(a.Value - b.Value);
    public static bool operator <(Quantity a, Quantity b) => a.Value < b.Value;
    public static bool operator >(Quantity a, Quantity b) => a.Value > b.Value;
    public static bool operator <=(Quantity a, Quantity b) => a.Value <= b.Value;
    public static bool operator >=(Quantity a, Quantity b) => a.Value >= b.Value;
}