using SneakerShop.ValueObjects.Base;
using SneakerShop.ValueObjects.Validators;

namespace SneakerShop.ValueObjects;

public class LogoUrl(string url) : ValueObject<string>(new LogoUrlValidator(), url);