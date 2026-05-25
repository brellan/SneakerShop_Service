using SneakerShop.ValueObjects.Base;
using SneakerShop.ValueObjects.Exceptions;

namespace SneakerShop.ValueObjects.Validators;

public class PriceValidator : IValidator<decimal>
{
    public void Validate(decimal value)
    {
        if (value <= 0)
            throw new InvalidBasePriceException(value);
    }
}