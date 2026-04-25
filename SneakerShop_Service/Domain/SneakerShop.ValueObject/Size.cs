using SneakerShop.ValueObjects.Base;
using SneakerShop.ValueObjects.Validators;

namespace SneakerShop.ValueObjects;

public class Size(float value) : ValueObject<float>(new SizeValidator(), value);