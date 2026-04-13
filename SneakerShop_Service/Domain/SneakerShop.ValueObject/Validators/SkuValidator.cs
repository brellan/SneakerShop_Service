using System.Text.RegularExpressions;
using SneakerShop.ValueObject.Base;
using SneakerShop.ValueObject.Exceptions;

namespace SneakerShop.ValueObject.Validators;

public class SkuValidator : IValidator<string>
{
    public static int ExactLength => 10;

    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));

        if (value.Length != ExactLength)
            throw new FormatException($"The \"{nameof(value)}\" must be exactly {ExactLength} characters");

        if (!Regex.IsMatch(value, @"^[A-Z0-9]+$"))
            throw new FormatException($"The \"{nameof(value)}\" must contain only uppercase letters and numbers");
    }
}