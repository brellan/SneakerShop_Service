namespace SneakerShop.ValueObjects.Exceptions;

public class InvalidBasePriceException(decimal price)
    : ArgumentException($"Base price {price} must be greater than zero");