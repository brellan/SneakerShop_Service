using SneakerShop.ValueObject.Base;
using SneakerShop.ValueObject.Exceptions;

namespace SneakerShop.ValueObject.Validators;

public class ProductNameValidator : IValidator<string>
{
    public static int MinLength => 3;
    public static int MaxLength => 200;

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