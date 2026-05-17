using SneakerShop.ValueObjects.Base;
using SneakerShop.ValueObjects.Validators;

namespace SneakerShop.ValueObjects;

public class TotalPrice(decimal amount) : ValueObject<decimal>(new TotalPriceValidator(), amount);