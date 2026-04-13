using SneakerShop.ValueObject.Base;
using SneakerShop.ValueObject.Validators;

namespace SneakerShop.ValueObject;

public class Description(string? value) : ValueObject<string>(new DescriptionValidator(), value ?? string.Empty);