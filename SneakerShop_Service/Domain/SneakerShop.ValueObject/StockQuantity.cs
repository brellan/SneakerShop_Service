using SneakerShop.ValueObjects.Base;
using SneakerShop.ValueObjects.Validators;

namespace SneakerShop.ValueObjects;

public class StockQuantity(int value) : ValueObject<int>(new StockQuantityValidator(), value)
{
    public static StockQuantity operator +(StockQuantity a, StockQuantity b) => new(a.Value + b.Value);
    public static StockQuantity operator -(StockQuantity a, StockQuantity b) => new(a.Value - b.Value);
    public static bool operator <(StockQuantity a, StockQuantity b) => a.Value < b.Value;
    public static bool operator >(StockQuantity a, StockQuantity b) => a.Value > b.Value;
    public static bool operator <=(StockQuantity a, StockQuantity b) => a.Value <= b.Value;
    public static bool operator >=(StockQuantity a, StockQuantity b) => a.Value >= b.Value;
}