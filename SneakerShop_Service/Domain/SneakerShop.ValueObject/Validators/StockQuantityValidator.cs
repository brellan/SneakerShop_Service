using SneakerShop.ValueObjects.Base;
using SneakerShop.ValueObjects.Exceptions;

namespace SneakerShop.ValueObjects.Validators;

public class StockQuantityValidator : IValidator<int>
{
    public static int MIN_STOCK => 0;
    public static int MAX_STOCK => 10000;

    public void Validate(int value)
    {
        if (value < MIN_STOCK || value > MAX_STOCK)
            throw new ArgumentException($"Stock quantity must be between {MIN_STOCK} and {MAX_STOCK}");
    }
}