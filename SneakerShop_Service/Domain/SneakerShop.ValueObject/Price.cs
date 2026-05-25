using SneakerShop.ValueObjects.Base;
using SneakerShop.ValueObjects.Validators;

namespace SneakerShop.ValueObjects;

public class Price(decimal amount) : ValueObject<decimal>(new PriceValidator(), amount);