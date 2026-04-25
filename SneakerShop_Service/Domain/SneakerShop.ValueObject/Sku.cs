using SneakerShop.ValueObjects.Base;
using SneakerShop.ValueObjects.Validators;

namespace SneakerShop.ValueObjects;

public class Sku(string code) : ValueObject<string>(new SkuValidator(), code);