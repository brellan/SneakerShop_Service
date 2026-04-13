using SneakerShop.ValueObject.Base;
using SneakerShop.ValueObject.Validators;

namespace SneakerShop.ValueObject;

public class LogoUrl(string value) : ValueObject<string>(new LogoUrlValidator(), value);