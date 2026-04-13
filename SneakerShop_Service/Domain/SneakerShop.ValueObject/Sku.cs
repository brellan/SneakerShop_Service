using SneakerShop.ValueObject.Base;
using SneakerShop.ValueObject.Validators;

namespace SneakerShop.ValueObject;

public class Sku(string value) : ValueObject<string>(new SkuValidator(), value);