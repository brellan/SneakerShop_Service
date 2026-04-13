using SneakerShop.ValueObject.Base;
using SneakerShop.ValueObject.Validators;

namespace SneakerShop.ValueObject;

public class AdditionalImages(string? value) : ValueObject<string>(new AdditionalImagesValidator(), value ?? string.Empty);