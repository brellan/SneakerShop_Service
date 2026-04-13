using SneakerShop.ValueObject.Base;
using SneakerShop.ValueObject.Exceptions;

namespace SneakerShop.ValueObject.Validators;

public class BrandNameValidator : IValidator<string>
{
    public static int MinLength => 2;
    public static int MaxLength => 100;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));

        if (value.Length < MinLength)
            throw new ArgumentShortValueException(nameof(value), value, MinLength);

        if (value.Length > MaxLength)
            throw new ArgumentLongValueException(nameof(value), value, MaxLength);
    }
}