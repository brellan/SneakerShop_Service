using SneakerShop.ValueObjects.Base;
using SneakerShop.ValueObjects.Validators;

namespace SneakerShop.ValueObjects;

public class ImageUrl(string url) : ValueObject<string>(new ImageUrlValidator(), url);