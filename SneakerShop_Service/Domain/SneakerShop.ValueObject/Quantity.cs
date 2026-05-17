using SneakerShop.ValueObjects.Base;
using SneakerShop.ValueObjects.Validators;

namespace SneakerShop.ValueObjects;

public class Quantity(int value) : ValueObject<int>(new QuantityValidator(), value);