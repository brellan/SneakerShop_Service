namespace SneakerShop.Domain.Exceptions;

public class InvalidPriceRangeException(decimal minPrice, decimal maxPrice)
    : ArgumentException($"Invalid price range: min {minPrice} must be less than or equal to max {maxPrice}")
{
    public decimal MinPrice => minPrice;
    public decimal MaxPrice => maxPrice;
}