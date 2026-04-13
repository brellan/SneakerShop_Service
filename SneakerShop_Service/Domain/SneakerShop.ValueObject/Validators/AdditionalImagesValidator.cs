using SneakerShop.ValueObject.Base;

namespace SneakerShop.ValueObject.Validators;

public class AdditionalImagesValidator : IValidator<string>
{
    public void Validate(string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            var urls = value.Split(',', StringSplitOptions.RemoveEmptyEntries);
            foreach (var url in urls)
            {
                if (!Uri.IsWellFormedUriString(url.Trim(), UriKind.Absolute))
                    throw new FormatException($"The \"{nameof(value)}\" contains invalid URL: {url}");
            }
        }
    }
}