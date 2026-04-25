using SneakerShop.ValueObjects.Base;
using SneakerShop.ValueObjects.Validators;

namespace SneakerShop.ValueObjects;

public class BrandName(string name) : ValueObject<string>(new BrandNameValidator(), name);