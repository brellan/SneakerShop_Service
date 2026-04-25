using SneakerShop.ValueObjects.Base;
using SneakerShop.ValueObjects.Validators;

namespace SneakerShop.ValueObjects;

public class ProductName(string name) : ValueObject<string>(new ProductNameValidator(), name);