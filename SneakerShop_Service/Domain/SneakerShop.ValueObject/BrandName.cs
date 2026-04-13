using SneakerShop.ValueObject.Base;
using SneakerShop.ValueObject.Validators;

namespace SneakerShop.ValueObject;

public class BrandName(string value) : ValueObject<string>(new BrandNameValidator(), value);