using SneakerShop.ValueObjects.Base;

namespace SneakerShop.ValueObjects.Validators;

public class TotalPriceValidator : IValidator<decimal>
{
    public void Validate(decimal value)
    {
        if (value < 0)
            throw new ArgumentException($"Total price cannot be negative. Current value: {value}");
    }
}