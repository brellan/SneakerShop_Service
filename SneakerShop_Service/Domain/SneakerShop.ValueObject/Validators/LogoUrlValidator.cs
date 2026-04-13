using SneakerShop.ValueObject.Base;
using SneakerShop.ValueObject.Exceptions;

namespace SneakerShop.ValueObject.Validators;

public class LogoUrlValidator : IValidator<string>
{
    public void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullOrWhiteSpaceException(nameof(value));

        if (!Uri.IsWellFormedUriString(value, UriKind.Absolute))
            throw new FormatException($"The \"{nameof(value)}\" is not a valid URL");
    }
}