namespace SneakerShop.ValueObjects.Exceptions;

public class InvalidSizeException(float size)
    : ArgumentException($"Size {size} must be between 1.0 and 55.0");