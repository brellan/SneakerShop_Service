using SneakerShop.ValueObjects.Base;
using SneakerShop.ValueObjects.Validators;

namespace SneakerShop.ValueObjects;

public class Description(string text) : ValueObject<string>(new DescriptionValidator(), text);