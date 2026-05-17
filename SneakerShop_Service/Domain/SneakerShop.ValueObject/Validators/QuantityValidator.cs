using SneakerShop.ValueObjects.Base;
using SneakerShop.ValueObjects.Exceptions;

namespace SneakerShop.ValueObjects.Validators;

public class QuantityValidator : IValidator<int>
{
    public static int MIN_QUANTITY => 1;
    public static int MAX_QUANTITY => 99;

    public void Validate(int value)
    {
        if (value < MIN_QUANTITY || value > MAX_QUANTITY)
            throw new ArgumentException($"Quantity must be between {MIN_QUANTITY} and {MAX_QUANTITY}");
    }
}