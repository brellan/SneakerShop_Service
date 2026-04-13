using SneakerShop.ValueObject.Base;
using SneakerShop.ValueObject.Exceptions;

namespace SneakerShop.ValueObject.Validators;

public class DescriptionValidator : IValidator<string>
{
    public static int MaxLength => 1000;

    public void Validate(string value)
    {
        if (value != null && value.Length > MaxLength)
            throw new ArgumentLongValueException(nameof(value), value, MaxLength);
    }
}