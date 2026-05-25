using SneakerShop.ValueObjects.Base;
using SneakerShop.ValueObjects.Exceptions;

namespace SneakerShop.ValueObjects.Validators;

public class DescriptionValidator : IValidator<string>
{
    public static int MAX_LENGTH => 2000;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));

        if (value.Length > MAX_LENGTH)
            throw new ArgumentLongValueException(nameof(value), value, MAX_LENGTH);
    }
}