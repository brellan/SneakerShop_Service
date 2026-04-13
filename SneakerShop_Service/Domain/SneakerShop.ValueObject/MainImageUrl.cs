using SneakerShop.ValueObject.Base;
using SneakerShop.ValueObject.Validators;

namespace SneakerShop.ValueObject;

public class MainImageUrl(string value) : ValueObject<string>(new MainImageUrlValidator(), value);