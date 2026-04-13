using System.Text.RegularExpressions;
using SneakerShop.ValueObject.Base;
using SneakerShop.ValueObject.Exceptions;

namespace SneakerShop.ValueObject.Validators;

public class SkuValidator : IValidator<string>
{
    public static int MinLength => 6;
    public static int MaxLength => 20;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));

        if (value.Length < MinLength)
            throw new ArgumentShortValueException(nameof(value), value, MinLength);

        if (value.Length > MaxLength)
            throw new ArgumentLongValueException(nameof(value), value, MaxLength);

        if (!Regex.IsMatch(value, @"^[A-Z0-9]+$"))
            throw new FormatException($"The \"{nameof(value)}\" must contain only uppercase letters and numbers");
    }
}