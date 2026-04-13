using SneakerShop.ValueObject.Base;
using SneakerShop.ValueObject.Validators;

namespace SneakerShop.ValueObject;

public class ProductNameAtCart(string value) : ValueObject<string>(new ProductNameAtCartValidator(), value);