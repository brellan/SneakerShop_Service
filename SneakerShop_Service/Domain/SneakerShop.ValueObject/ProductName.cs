using SneakerShop.ValueObject.Base;
using SneakerShop.ValueObject.Validators;

namespace SneakerShop.ValueObject;

public class ProductName(string value) : ValueObject<string>(new ProductNameValidator(), value);