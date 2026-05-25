using SneakerShop.ValueObjects.Base;
using SneakerShop.ValueObjects.Validators;

namespace SneakerShop.ValueObjects;

public class StockQuantity(int value) : ValueObject<int>(new StockQuantityValidator(), value);