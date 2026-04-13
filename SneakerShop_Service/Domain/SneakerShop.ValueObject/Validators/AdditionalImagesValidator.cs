using SneakerShop.ValueObject.Base;

namespace SneakerShop.ValueObject.Validators;

public class AdditionalImagesValidator : IValidator<string>
{
    public void Validate(string value)
    {
        if (value != null && !Uri.IsWellFormedUriString(value, UriKind.Absolute))
            throw new FormatException($"The \"{nameof(value)}\" is not a valid URL");
    }
}